import { action, computed, observable } from "mobx";
import { RootStore } from "./root";
import { ILogin, IProfile } from "../models/profile";
import Profile from "../api/profile";
import { history } from "../..";
import { toast } from "react-toastify";
import { getErrors } from "../common/util/util";

export default class ProfileStore {
  rootStore: RootStore;

  constructor(rootStore: RootStore) {
    this.rootStore = rootStore;
  }

  @observable user: IProfile | undefined;

  @computed get isLoggedIn() {
    return !!this.user;
  }

  @action current = async () => {
    try {
      const user = await Profile.current();
      this.user = user;
    } catch (error) {
      console.log(error);
      if (error && error.status !== 500 ) toast.error(getErrors(error));
    }
  };

  @action login = async (values: ILogin) => {
    try {
      const login = await Profile.login(values);
      this.rootStore.commonStore.setToken(login.token);
      this.user = await Profile.current();

      history.push("");
    } catch (error) {
      throw error;
    }
  };

  @action logout = () => {
    this.rootStore.commonStore.setToken(undefined);
    this.user = undefined;
    history.push("login");
  };
}
