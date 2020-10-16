import { action, observable, reaction } from "mobx";
import { RootStore } from "./rootStore";

export default class CommonStore {
  rootStore: RootStore;

  constructor(rootStore: RootStore) {
    this.rootStore = rootStore;

    reaction(
      () => this.token,
      (token) => {
        if (token) window.localStorage.setItem("jwt", token);
        else window.localStorage.removeItem("jwt");
      }
    );
  }

  @observable collapsed = true;

  @observable token: string | undefined =
    window.localStorage.getItem("jwt") ?? undefined;

  @action setCollapsed = () => {
    this.collapsed = !this.collapsed;
  }

  @action setToken = (token: string | undefined) => {
    this.token = token;
  };
}
