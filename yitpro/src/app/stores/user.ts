import { observable, action, autorun, reaction } from "mobx";
import { toast } from "react-toastify";
import { history } from "../..";
import User from "../api/user";
import { filterByText, getErrors } from "../common/util/util";
import { Messages } from "../models/messages";
import {
  IUser,
  IUserDetail,
  IUserFilter,
  IUserPermission,
  UserFilterValues,
} from "../models/user";
import { RootStore } from "./root";

export default class UserStore {
  rootStore: RootStore;

  constructor(rootStore: RootStore) {
    this.rootStore = rootStore;

    reaction(
      () => this.users.slice(),
      () => {
        this.filtered = this.users;
      }
    );

    autorun(() => {
      this.countCheckPermissions();
    });
  }

  @observable user: IUserDetail | undefined;
  @observable userId: number = 0;
  @observable users: IUser[] = [];
  @observable filter: IUserFilter = new UserFilterValues();
  @observable loading = false;
  @observable submitting = false;
  @observable filtered: IUser[] = [];
  @observable permissions: IUserPermission[] = [];
  @observable totalAccessChecked: "indeterminate" | "all" | "none" =
    "indeterminate";
  @observable totalCreateChecked: "indeterminate" | "all" | "none" =
    "indeterminate";
  @observable totalUpdateChecked: "indeterminate" | "all" | "none" =
    "indeterminate";
  @observable totalDeleteChecked: "indeterminate" | "all" | "none" =
    "indeterminate";

  @action clearFilter = () => {
    this.filter = new UserFilterValues();
  };

  @action clearPermissions = () => {
    this.permissions = [];
  };

  @action clearUsers = () => {
    this.users = [];
  };

  @action clearUser = () => {
    this.userId = 0;
    this.user = undefined;
  };

  @action initForm = async () => {
    this.rootStore.optionStore.getRoleOptions();
    this.rootStore.optionStore.getLineManagersOptions();
  };

  @action setUserId = (id: number) => {
    this.userId = id;
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
      await User.index();
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
      const users = await User.get();
      this.users = users;
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.loading = false;
    }
  };

  @action getDetail = async () => {
    try {
      this.submitting = true;
      const user = await User.getDetail(this.userId);
      user.admissionDate = new Date(user.admissionDate!.toString());
      this.user = user;
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.submitting = false;
    }
  };

  @action getPermissions = async () => {
    this.submitting = true;
    try {
      const permissions = await User.getPermissions(this.userId);
      this.permissions = permissions;
    } catch (error) {
      toast.error(getErrors(error));
    } finally {
      this.submitting = false;
    }
  };

  @action filterUsers = async () => {
    this.loading = true;
    try {
      this.filter.active =
        this.filter.active === "" ? undefined : this.filter.active;
      this.filter.roleId =
        this.filter.roleId === "" ? undefined : this.filter.roleId;
      const users = await User.getFiltered(this.filter);
      this.users = users;
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.loading = false;
    }
  };

  @action filterByText = (filterString: string = "") => {
    const keys: Array<keyof IUser> = ["name", "role", "email"];
    this.filtered = this.users.filter(filterByText(keys, filterString));
  };

  @action post = async (user: FormData) => {
    this.submitting = true;
    try {
      const id = await User.post(user);
      toast.success(Messages.postSuccess);
      const newUser = await User.getById(id);
      this.users.push(newUser);
      this.rootStore.modalStore.closeModal();
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.submitting = false;
    }
  };

  @action put = async (user: FormData) => {
    this.submitting = true;
    try {
      await User.put(user);
      toast.success(Messages.putSuccess);
      const updatedUser = await User.getById(Number(user.get("id")));
      const index = this.users.findIndex((x) => x.id === updatedUser.id);
      if (index !== -1) this.users[index] = updatedUser;
      this.rootStore.modalStore.closeModal();
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.submitting = false;
    }
  };

  @action putEnabled = async (user: IUser) => {
    try {
      await User.putEnabled(user);
      toast.success(Messages.putSuccess);
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
      throw error;
    }
  };

  @action putPermissions = async (permissions: IUserPermission[]) => {
    this.submitting = true;
    try {
      await User.putPermissions(permissions);
      toast.success(Messages.putSuccess);
      this.rootStore.modalStore.closeModal();
    } catch (error) {
      toast.error(getErrors(error));
    } finally {
      this.submitting = false;
    }
  };

  @action download = async () => {
    this.loading = true;
    try {
      await User.download();
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
