import { observable, action } from "mobx";
import { toast } from "react-toastify";
import Option from "../api/options";
import { getErrors } from "../common/util/util";
import { IOption } from "../models/options";
import { RootStore } from "./rootStore";

export default class OptionStore {
  rootStore: RootStore;

  constructor(rootStore: RootStore) {
    this.rootStore = rootStore;
  }

  @observable roleOptions: IOption[] = [];
  @observable lineManagersOptions: IOption[] = [];

  @action getRoleOptions = async () => {
    try {
      const options = await Option.getRoles();
      this.roleOptions = options;
    } catch (error) {
      if (error &&  error?.status !== 500) toast.error(getErrors(error));
    }
  };

  @action getLineManagersOptions = async () => {
    try {
      const options = await Option.getLineManagers();
      this.lineManagersOptions = options;
    } catch (error) {
      if (error &&  error?.status !== 500) toast.error(getErrors(error));
    }
  };
}
