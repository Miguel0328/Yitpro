import { observable, action } from "mobx";
import { toast } from "react-toastify";
import Option from "../api/options";
import { getErrors } from "../common/util/util";
import { GC } from "../models/catalog";
import { IOption } from "../models/common";
import { RootStore } from "./root";

export default class OptionStore {
  rootStore: RootStore;

  constructor(rootStore: RootStore) {
    this.rootStore = rootStore;
  }

  @observable loadingRoles = false;
  @observable loadingClients = false;
  @observable loadingManagers = false;
  @observable loadingResponsibles = false;
  @observable loadingCatalogs = false;
  @observable loadingDepartments = false;
  @observable loadingProjectCatalogs = false;
  @observable loadingProjectPhases = false;
  @observable loadingProjects = false;
  @observable loadingClasifications = false;
  @observable loadingProjectTeam = false;

  @observable roleOptions: IOption[] = [];
  @observable clientOptions: IOption[] = [];
  @observable catalogOptions: IOption[] = [];
  @observable departmentOptions: IOption[] = [];
  @observable managerOptions: IOption[] = [];
  @observable responsibleOptions: IOption[] = [];
  @observable projectMethodologyOptions: IOption[] = [];
  @observable projectTypeOptions: IOption[] = [];
  @observable projectStatusOptions: IOption[] = [];
  @observable projectPhaseOptions: IOption[] = [];
  @observable projectOptions: IOption[] = [];
  @observable clasificationOptions: IOption[] = [];
  @observable projectTeamOptions: IOption[] = [];

  @action getRoleOptions = async () => {
    this.loadingRoles = true;
    try {
      if (Array.from(this.roleOptions).length === 0) {
        const options = await Option.getRoles();
        this.roleOptions = options;
      }
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.loadingRoles = false;
    }
  };

  @action getClientOptions = async () => {
    this.loadingClients = true;
    try {
      const options = await Option.getClients();
      this.clientOptions = options;
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.loadingClients = false;
    }
  };

  @action getCatalogOptions = async () => {
    this.loadingCatalogs = true;
    try {
      const options = await Option.getCatalogs();
      this.catalogOptions = options;
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.loadingCatalogs = false;
    }
  };

  @action getDepartmentOptions = async () => {
    this.loadingDepartments = true;
    try {
      const options = await Option.getCatalogs(GC.department);
      this.departmentOptions = options;
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.loadingDepartments = false;
    }
  };

  @action getProjectMethodologyOptions = async () => {
    this.loadingProjectCatalogs = true;
    try {
      const options = await Option.getCatalogs(GC.projectMethodology);
      this.projectMethodologyOptions = options;
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.loadingProjectCatalogs = false;
    }
  };

  @action getProjectStatusOptions = async () => {
    this.loadingProjectCatalogs = true;
    try {
      const options = await Option.getCatalogs(GC.projectStatus);
      this.projectStatusOptions = options;
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.loadingProjectCatalogs = false;
    }
  };

  @action getProjectTypeOptions = async () => {
    this.loadingProjectCatalogs = true;
    try {
      const options = await Option.getCatalogs(GC.projectType);
      this.projectTypeOptions = options;
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.loadingProjectCatalogs = false;
    }
  };

  @action getProjectPhaseOptions = async () => {
    this.loadingProjectPhases = true;
    try {
      const options = await Option.getCatalogs(GC.projectPhase);
      this.projectPhaseOptions = options;
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.loadingProjectPhases = false;
    }
  };

  @action clearClasificationOptions = () => {
    this.clasificationOptions = [];
  };
  @action getClasificationOptions = async (id: number) => {
    this.loadingClasifications = true;
    try {
      const options = await Option.getClasifications(id);
      this.clasificationOptions = options;
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.loadingClasifications = false;
    }
  };

  @action clearProjectTeamOptions = () => {
    this.projectTeamOptions = [];
  };
  @action getProjectTeamOptions = async (id: number) => {
    this.loadingProjectTeam = true;
    try {
      const options = await Option.getProjectTeam(id);
      this.projectTeamOptions = options;
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.loadingProjectTeam = false;
    }
  };

  @action getProjectOptions = async () => {
    this.loadingProjects = true;
    try {
      const options = await Option.getProjects();
      this.projectOptions = options;
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.loadingProjects = false;
    }
  };

  @action getManagerOptions = async () => {
    this.loadingManagers = true;
    try {
      const options = await Option.getManagers();
      this.managerOptions = options;
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.loadingManagers = false;
    }
  };

  @action getResponsibleOptions = async () => {
    this.loadingResponsibles = true;
    try {
      const options = await Option.getResponsibles();
      this.responsibleOptions = options;
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.loadingResponsibles = false;
    }
  };
}
