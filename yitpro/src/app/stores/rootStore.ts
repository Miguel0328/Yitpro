import { createContext } from "react";
import CommonStore from "./commonStore";
import ModalStore from "./modalStore";
import RoleStore from "./roleStore";
import ProfileStore from "./profileStore";
import UserStore from "./userStore";
import OptionStore from "./optionStore";

export class RootStore {
  profileStore: ProfileStore;
  commonStore: CommonStore;
  roleStore: RoleStore;
  modalStore: ModalStore;
  userStore: UserStore;
  optionStore: OptionStore;

  constructor() {
    this.profileStore = new ProfileStore(this);
    this.commonStore = new CommonStore(this);
    this.roleStore = new RoleStore(this);
    this.modalStore = new ModalStore(this);
    this.userStore = new UserStore(this);
    this.optionStore = new OptionStore(this);
  }
}

export const RootStoreContext = createContext(new RootStore());
