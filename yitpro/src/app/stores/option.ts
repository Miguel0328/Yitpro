import { observable, action } from "mobx";
import { toast } from "react-toastify";
import Option from "../api/options";
import { getErrors } from "../common/util/util";
import { IOption } from "../models/options";
import { RootStore } from "./root";

export default class OptionStore {
  rootStore: RootStore;

  constructor(rootStore: RootStore) {
    this.rootStore = rootStore;
  }

  @observable roleOptions: IOption[] = [];
  @observable clientOptions: IOption[] = [];
  @observable lineManagersOptions: IOption[] = [];

  @action getRoleOptions = async () => {
    try {
      if (Array.from(this.roleOptions).length === 0) {
        const options = await Option.getRoles();
        this.roleOptions = options;
      }
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    }
  };

  @action getClientOptions = async () => {
    try {
      if (Array.from(this.clientOptions).length === 0) {
        const options = await Option.getClients();
        this.clientOptions = options;
      }
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    }
  };

  @action getLineManagersOptions = async () => {
    try {
      if (Array.from(this.roleOptions).length === 0) {
        const options = await Option.getLineManagers();
        this.lineManagersOptions = options;
      }
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    }
  };
}
