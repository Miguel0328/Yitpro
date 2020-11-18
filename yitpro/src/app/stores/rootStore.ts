import { createContext } from "react";
import CommonStore from "./commonStore";
import ModalStore from "./modalStore";
import RoleStore from "./roleStore";
import ProfileStore from "./profileStore";

export class RootStore {
  profileStore: ProfileStore;
  commonStore: CommonStore;
  roleStore: RoleStore;
  modalStore: ModalStore;

  constructor() {
    this.profileStore = new ProfileStore(this);
    this.commonStore = new CommonStore(this);
    this.roleStore = new RoleStore(this);
    this.modalStore = new ModalStore(this);
  }
}

export const RootStoreContext = createContext(new RootStore());
