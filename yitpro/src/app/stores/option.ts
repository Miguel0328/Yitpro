import { observable, action } from "mobx";
import { toast } from "react-toastify";
import Option from "../api/options";
import { getErrors } from "../common/util/util";
import { IOption } from "../models/common";
import { RootStore } from "./root";

export default class OptionStore {
  rootStore: RootStore;

  constructor(rootStore: RootStore) {
    this.rootStore = rootStore;
  }

  @observable loadingRoles = false;
  @observable loadingClients = false;
  @observable loadingManagers = false;
  @observable loadingCatalogs = false;

  @observable roleOptions: IOption[] = [];
  @observable clientOptions: IOption[] = [];
  @observable catalogOptions: IOption[] = [];
  @observable lineManagersOptions: IOption[] = [];

  @action getRoleOptions = async () => {
    this.loadingRoles = true;
    try {
      if (Array.from(this.roleOptions).length === 0) {
        const options = await Option.getRoles();
        this.roleOptions = options;
      }
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.loadingRoles = false;
    }
  };

  @action getClientOptions = async () => {
    this.loadingClients = false;
    try {
      if (Array.from(this.clientOptions).length === 0) {
        const options = await Option.getClients();
        this.clientOptions = options;
      }
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.loadingClients = false;
    }
  };

  @action getCatalogOptions = async () => {
    this.loadingCatalogs = false;
    try {
      if (Array.from(this.catalogOptions).length === 0) {
        const options = await Option.getCatalogs();
        this.catalogOptions = options;
      }
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.loadingCatalogs = false;
    }
  };

  @action getManagerOptions = async () => {
    this.loadingManagers = true;
    try {
      if (Array.from(this.roleOptions).length === 0) {
        const options = await Option.getManagers();
        this.lineManagersOptions = options;
      }
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.loadingManagers = false;
    }
  };
}
