import { observable, action, reaction, autorun } from "mobx";
import { toast } from "react-toastify";
import { history } from "../..";
import Role from "../api/role";
import { getErrors } from "../common/util/util";
import { Messages } from "../models/messages";
import { IRole, IRoleFilter, IRolePermission } from "../models/role";
import { RootStore } from "./rootStore";

export default class RoleStore {
  rootStore: RootStore;

  constructor(rootStore: RootStore) {
    this.rootStore = rootStore;

    reaction(
      () => this.roles,
      () => {
        this.filterRoles();
      }
    );

    autorun(() => {
      this.countCheckPermissions();
    });
  }

  @observable role: IRole | undefined;
  @observable roles: IRole[] = [];
  @observable filter: IRoleFilter = {
    role: "",
    active: "all",
    protected: "all",
  };
  @observable loading = false;
  @observable submitting = false;
  @observable filtered: IRole[] = [];
  @observable permissions: IRolePermission[] = [];
  @observable totalAccessChecked: "indeterminate" | "all" | "none" =
    "indeterminate";
  @observable totalCreateChecked: "indeterminate" | "all" | "none" =
    "indeterminate";
  @observable totalUpdateChecked: "indeterminate" | "all" | "none" =
    "indeterminate";
  @observable totalDeleteChecked: "indeterminate" | "all" | "none" =
    "indeterminate";

  getRole = (id: number): IRole | undefined => {
    return this.roles.find((x) => x.id === id);
  };

  @action clearFilter = () => {
    this.filter = { role: "", active: "all", protected: "all" };
  };

  @action clearPermissions = () => {
    this.permissions = [];
  };

  @action clearRoles = () => {
    this.roles = [];
  };

  @action setRole = (id: number) => {
    let role = this.getRole(id);
    this.role = role;
  };

  @action setFilter = (e: any, result: any = null) => {
    const { name, value } = result || e.target;

    this.filter = {
      ...this.filter,
      [name]: value,
    };
  };

  @action setAllChecked = (
    value: boolean,
    type: "access" | "create" | "update" | "delete"
  ) => {
    this.permissions = this.permissions.map((p) => {
      p[type] = value;
      return p;
    });
  };

  @action index = async () => {
    this.rootStore.commonStore.loadingIndex = true;
    try {
      await Role.index();
    } catch (error) {
      history.push("/forbidden");
      throw error;
    } finally {
      this.rootStore.commonStore.loadingIndex = false;
    }
  };

  @action get = async () => {
    this.loading = true;
    try {
      const roles = await Role.get();
      this.roles = roles;
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.loading = false;
    }
  };

  @action getPermissions = async (id: number) => {
    this.submitting = true;
    try {
      const permissions = await Role.getPermissions(id);
      this.permissions = permissions;
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.submitting = false;
    }
  };

  @action filterRoles = () => {
    this.filtered = this.roles.filter(
      (x) =>
        x.active ===
          (this.filter.active === "all"
            ? x.active
            : this.filter.active === "yes"
            ? true
            : false) &&
        x.protected ===
          (this.filter.protected === "all"
            ? x.protected
            : this.filter.protected === "yes"
            ? true
            : false) &&
        (x.name.toLowerCase().includes(this.filter.role.toLowerCase()) ||
          this.filter.role === "")
    );
  };

  @action post = async (role: IRole) => {
    this.submitting = true;
    try {
      await Role.post(role);
      toast.success(Messages.postSuccess);
      this.get();
      this.rootStore.modalStore.closeModal();
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.submitting = false;
    }
  };

  @action put = async (role: IRole) => {
    this.submitting = true;
    try {
      await Role.put(role);
      toast.success(Messages.putSuccess);
      this.get();
      this.rootStore.modalStore.closeModal();
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.submitting = false;
    }
  };

  @action putPermissions = async (permissions: IRolePermission[]) => {
    this.submitting = true;
    try {
      await Role.putPermissions(permissions);
      toast.success(Messages.putSuccess);
      this.rootStore.modalStore.closeModal();
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.submitting = false;
    }
  };

  @action putEnabled = async (role: IRole) => {
    try {
      await Role.put(role);
      toast.success(Messages.putSuccess);
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
      throw error;
    }
  };

  countCheckPermissions = () => {
    const total = this.permissions.length;
    const accessChecked = this.permissions.filter((x) => x.access).length;
    const createChecked = this.permissions.filter((x) => x.create).length;
    const updateChecked = this.permissions.filter((x) => x.update).length;
    const deleteChecked = this.permissions.filter((x) => x.delete).length;

    this.totalAccessChecked =
      accessChecked === 0
        ? "none"
        : accessChecked > 0 && accessChecked < total
        ? "indeterminate"
        : "all";

    this.totalCreateChecked =
      createChecked === 0
        ? "none"
        : createChecked > 0 && createChecked < total
        ? "indeterminate"
        : "all";

    this.totalUpdateChecked =
      updateChecked === 0
        ? "none"
        : updateChecked > 0 && updateChecked < total
        ? "indeterminate"
        : "all";

    this.totalDeleteChecked =
      deleteChecked === 0
        ? "none"
        : deleteChecked > 0 && deleteChecked < total
        ? "indeterminate"
        : "all";
  };
}
