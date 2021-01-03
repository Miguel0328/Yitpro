import { observable, action, reaction, autorun } from "mobx";
import { toast } from "react-toastify";
import { history } from "../..";
import Role from "../api/role";
import { filterByText, getErrors } from "../common/util/util";
import { Messages } from "../models/messages";
import { IRole, IRoleFilter, IRolePermission, RoleFilterValues } from "../models/role";
import { RootStore } from "./root";

export default class RoleStore {
  rootStore: RootStore;

  constructor(rootStore: RootStore) {
    this.rootStore = rootStore;

    reaction(
      () => this.roles.slice(),
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
  @observable filter: IRoleFilter = new RoleFilterValues();
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
    return this.filtered.find((x) => x.id === id);
  };

  @action clearFilter = () => {
    this.filter = new RoleFilterValues();
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

  @action filterRoles = (filterString: string = "") => {
    const keys: Array<keyof IRole> = ["name", "protected"];
    this.filtered = this.roles
      .filter(
        (x) =>
          x.active ===
            (this.filter.active === ""
              ? x.active
              : this.filter.active === "yes"
              ? true
              : false) &&
          x.protected ===
            (this.filter.protected === ""
              ? x.protected
              : this.filter.protected === "yes"
              ? true
              : false) &&
          (x.name.toLowerCase().includes(this.filter.role.toLowerCase()) ||
            this.filter.role === "")
      )
      .filter(filterByText(keys, filterString));
  };

  @action post = async (role: IRole) => {
    this.submitting = true;
    console.log("aqui");
    try {
      role.id = await Role.post(role);
      toast.success(Messages.postSuccess);
      this.roles.push(role);
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
      const index = this.roles.findIndex((x) => x.id === role.id);
      if (index !== -1) this.roles[index] = role;
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

  @action download = async () => {
    this.loading = true;
    try {
      await Role.download();
    } catch (error) {
      toast.error(getErrors(error));
    } finally {
      this.loading = false;
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
