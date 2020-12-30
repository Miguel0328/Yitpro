import { observable, action, reaction } from "mobx";
import { toast } from "react-toastify";
import Project from "../../api/project";
import { filterByText, getErrors } from "../../common/util/util";
import {
  IProjectTeam,
} from "../../models/project";
import { RootStore } from "../root";
import ProjectStore from "./project";

export default class ProjectTeamStore extends ProjectStore {
  rootStore: RootStore;

  constructor(rootStore: RootStore) {
    super(rootStore);
    this.rootStore = rootStore;

    reaction(
      () => this.team.slice(),
      () => {
        this.filteredTeam = this.team;
      }
    );
  }

  @observable filteredTeam: IProjectTeam[] = [];
  @observable team: IProjectTeam[] = [];
  @observable selected: number[] = [];

  @action clearTeam = () => {
    this.team = [];
  };

  @action setSelected = (selected: number[]) => {
    this.selected = selected;
  };

  @action filterByText = (filterString: string = "") => {
    const keys: Array<keyof IProjectTeam> = ["user"];
    this.filteredTeam = this.team.filter(filterByText(keys, filterString));
  };

  @action getTeam = async () => {
    try {
      this.loading = true;
      this.team = await Project.getTeam(this.projectId);
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.loading = false;
    }
  };

  @action download = async () => {
    this.loading = true;
    try {
      await Project.download();
    } catch (error) {
      toast.error(getErrors(error));
    } finally {
      this.loading = false;
    }
  };
}
