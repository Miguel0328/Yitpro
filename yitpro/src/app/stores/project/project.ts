import { observable, action, reaction } from "mobx";
import { toast } from "react-toastify";
import { history } from "../../..";
import Project from "../../api/project";
import { filterByText, getErrors } from "../../common/util/util";
import { Messages } from "../../models/messages";
import {
  IProject,
  IProjectDetail,
  IProjectFilter,
  ProjectFilterValues,
} from "../../models/project";
import { RootStore } from "../root";

export default class ProjectStore {
  rootStore: RootStore;

  constructor(rootStore: RootStore) {
    this.rootStore = rootStore;

    reaction(
      () => this.projects.slice(),
      () => {
        this.filtered = this.projects;
      }
    );
  }

  @observable project: IProjectDetail | undefined;
  @observable projectId: number = 0;
  @observable projectCode: string = "";
  @observable projectName: string = "";
  @observable projects: IProject[] = [];
  @observable filter: IProjectFilter = new ProjectFilterValues();
  @observable loading = false;
  @observable loadingDetail = false;
  @observable submitting = false;
  @observable filtered: IProject[] = [];

  @action clearFilter = () => {
    this.filter = new ProjectFilterValues();
  };

  @action clearProjects = () => {
    this.projects = [];
  };

  @action clearProject = () => {
    this.project = undefined;
  };

  @action clearProjectName = () => {
    this.projectName = "";
  };

  @action clearProjectId = () => {
    this.projectId = 0;
  }

  @action initForm = async () => {
    this.rootStore.optionStore.getClientOptions();
    this.rootStore.optionStore.getManagerOptions();
    this.rootStore.optionStore.getProjectTypeOptions();
    this.rootStore.optionStore.getProjectMethodologyOptions();
    this.rootStore.optionStore.getProjectStatusOptions();
  };

  @action setProjectCode = (code: string) => {
    this.projectCode = code;
  };

  @action setProjectId = (id: number) => {
    this.projectId = id;
  };

  @action setFilter = (e: any, result: any = null) => {
    const { name, value } = result || e.target;

    this.filter = {
      ...this.filter,
      [name]: value,
    };
  };

  @action index = async () => {
    this.rootStore.commonStore.loadingIndex = true;
    try {
      await Project.index();
    } catch (error) {
      history.push("/forbidden");
      throw error;
    } finally {
      this.rootStore.commonStore.loadingIndex = false;
    }
  };

  @action indexDetail = async () => {
    this.rootStore.commonStore.loadingIndex = true;
    try {
      await Project.indexDetial(this.projectId);
    } catch (error) {
      history.push("/forbidden");
      throw error;
    } finally {
      this.rootStore.commonStore.loadingIndex = false;
    }
  };

  @action get = async () => {
    this.loading = true;
    try {
      const projects = await Project.get();
      this.projects = projects;
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.loading = false;
    }
  };

  @action getId = async () => {
    try {
      this.rootStore.commonStore.loadingIndex = true;
      this.projectId = await Project.getId(this.projectCode);
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.rootStore.commonStore.loadingIndex = false;
    }
  };

  @action getDetail = async () => {
    try {
      this.submitting = true;
      const project = await Project.getDetail(this.projectId);
      this.project = project;
      this.projectName = project.name;
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.submitting = false;
    }
  };

  @action filterProjects = async () => {
    this.loading = true;
    try {
      this.filter.active =
        this.filter.active === "" ? undefined : this.filter.active;
      this.filter.clientId =
        this.filter.clientId === "" ? undefined : this.filter.clientId;
      const projects = await Project.getFiltered(this.filter);
      this.projects = projects;
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.loading = false;
    }
  };

  @action filterByText = (filterString: string = "") => {
    const keys: Array<keyof IProject> = ["name", "code", "leader", "client"];
    this.filtered = this.projects.filter(filterByText(keys, filterString));
  };

  @action post = async (project: IProjectDetail) => {
    this.submitting = true;
    try {
      const id = await Project.post(project);
      toast.success(Messages.postSuccess);
      const newProject = await Project.getById(id);
      this.projects.push(newProject);
      this.rootStore.modalStore.closeModal();
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.submitting = false;
    }
  };

  @action put = async (project: IProjectDetail) => {
    this.submitting = true;
    try {
      await Project.put(project, project.id);
      toast.success(Messages.putSuccess);
      const updatedProject = await Project.getById(project.id);
      const index = this.projects.findIndex((x) => x.id === updatedProject.id);
      if (index !== -1) this.projects[index] = updatedProject;
      this.rootStore.modalStore.closeModal();
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.submitting = false;
    }
  };

  @action putEnabled = async (project: IProject) => {
    try {
      await Project.putEnabled(project, project.id);
      toast.success(Messages.putSuccess);
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
      throw error;
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
