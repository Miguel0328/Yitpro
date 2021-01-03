import { observable, action, reaction, toJS } from "mobx";
import { toast } from "react-toastify";
import Project from "../../api/project";
import { filterByText, getErrors } from "../../common/util/util";
import { Messages } from "../../models/messages";
import { IProjectTeam } from "../../models/project";
import { IUser } from "../../models/user";
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

    reaction(
      () => this.remaining.slice(),
      () => {
        this.filteredRemaining = this.remaining;
      }
    );

    reaction(
      () => this.rootStore.projectStore.projectId,
      () => {
        this.projectId = this.rootStore.projectStore.projectId;
      }
    );
  }

  @observable filteredTeam: IProjectTeam[] = [];
  @observable filteredRemaining: IUser[] = [];
  @observable team: IProjectTeam[] = [];
  @observable remaining: IUser[] = [];
  @observable selected: number[] = [];
  @observable selectedRemaining: number[] = [];

  @action clearTeam = () => {
    this.team = [];
  };

  @action clearRemaining = () => {
    this.remaining = [];
  };

  @action setSelected = (selected: number[]) => {
    this.selected = selected;
  };

  @action setSelectedRemaining = (selected: number[]) => {
    this.selectedRemaining = selected;
  };

  @action filterByText = (filterString: string = "") => {
    const keys: Array<keyof IProjectTeam> = ["user"];
    this.filteredTeam = this.team.filter(filterByText(keys, filterString));
  };

  @action filterRemainingByText = (filterString: string = "") => {
    const keys: Array<keyof IUser> = ["name"];
    this.filteredRemaining = this.remaining.filter(
      filterByText(keys, filterString)
    );
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

  @action getRemaining = async () => {
    try {
      this.submitting = true;
      this.remaining = await Project.getRemainingTeam(this.projectId);
      console.log(toJS(this.remaining));
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.submitting = false;
    }
  };

  @action postTeam = async () => {
    this.submitting = true;
    try {
      const team = {
        id: this.projectId,
        ids: this.selectedRemaining,
      };
      await Project.postTeam(team, this.projectId);
      toast.success(Messages.postSuccess);
      this.rootStore.modalStore.closeModal();
      this.getTeam();
      this.setSelectedRemaining([]);
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.submitting = false;
    }
  };

  @action deleteTeam = async () => {
    this.loading = true;
    try {
      const team = {
        id: this.projectId,
        ids: this.selected,
      };
      await Project.deleteTeam(team, this.projectId);
      toast.success(Messages.deleteSuccess);
      this.rootStore.modalStore.closeModal();
      this.team = this.team.filter((x) => !team.ids.includes(x.id));
      this.setSelected([]);
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.loading = false;
    }
  };

  @action downloadTeam = async () => {
    this.loading = true;
    try {
      await Project.downloadTeam(this.projectId);
    } catch (error) {
      toast.error(getErrors(error));
    } finally {
      this.loading = false;
    }
  };
}
