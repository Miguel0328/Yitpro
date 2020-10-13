import { createContext } from "react";
import CommonStore from "./commonStore";
import UserStore from "./userStore";

export class RootStore {
  userStore: UserStore;
  commonStore: CommonStore;

  constructor() {
    this.userStore = new UserStore(this);
    this.commonStore = new CommonStore(this);
  }
}

export const RootStoreContext = createContext(new RootStore());
