import { createContext } from "react";
import CommonStore from "./common";
import ModalStore from "./modal";
import RoleStore from "./role";
import ProfileStore from "./profile";
import UserStore from "./user";
import OptionStore from "./option";
import ClientStore from "./client";
import ProjectStore from "./project/project";
import ProjectTeamStore from "./project/projectTeam";
import CatalogStore from "./catalog";
import PhaseStore from "./phase";
import ActivityStore from "./activity/activity";

export class RootStore {
  profileStore: ProfileStore;
  commonStore: CommonStore;
  roleStore: RoleStore;
  catalogStore: CatalogStore;
  clientStore: ClientStore;
  phaseStore: PhaseStore;
  projectStore: ProjectStore;
  activityStore: ActivityStore;
  projectTeamStore: ProjectTeamStore;
  modalStore: ModalStore;
  userStore: UserStore;
  optionStore: OptionStore;

  constructor() {
    this.profileStore = new ProfileStore(this);
    this.commonStore = new CommonStore(this);
    this.roleStore = new RoleStore(this);
    this.catalogStore = new CatalogStore(this);
    this.clientStore = new ClientStore(this);
    this.phaseStore = new PhaseStore(this);
    this.projectStore = new ProjectStore(this);
    this.activityStore = new ActivityStore(this);
    this.projectTeamStore = new ProjectTeamStore(this);
    this.modalStore = new ModalStore(this);
    this.userStore = new UserStore(this);
    this.optionStore = new OptionStore(this);
  }
}

export const RootStoreContext = createContext(new RootStore());