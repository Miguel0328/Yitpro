import { action, observable } from "mobx";
import { RootStore } from "./rootStore";
import { ILogin, IUser } from "../models/user";
import User from "../api/user";

export default class UserStore {
  rootStore: RootStore;

  constructor(rootStore: RootStore) {
    this.rootStore = rootStore;
  }

  @observable user: IUser | undefined;

  @action login = async (values: ILogin) => {
    try {
      const user = await User.login(values);
      this.user = user;
      this.rootStore.commonStore.setToken(user.token);
    } catch (error) {
      throw error;
    }
  };
}
