import { observable, action, reaction } from "mobx";
import { toast } from "react-toastify";
// import { history } from "../..";
import Phase from "../api/phase";
import { filterByText, getErrors } from "../common/util/util";
import { Messages } from "../models/messages";
import {
  //   PhaseFilterValues,
  IPhase,
  //   IPhaseFilter,
  IClasification,
} from "../models/phase";
import { RootStore } from "./root";

export default class PhaseStore {
  rootStore: RootStore;

  constructor(rootStore: RootStore) {
    this.rootStore = rootStore;

    reaction(
      () => this.phases.slice(),
      () => {
        this.filterPhases();
      }
    );

    reaction(
      () => this.clasifications.slice(),
      () => {
        this.filterClasifications();
      }
    );
  }

  @observable phase: IPhase | undefined;
  @observable phases: IPhase[] = [];
  @observable clasifications: IClasification[] = [];
  //   @observable filter: IPhaseFilter = new PhaseFilterValues();
  @observable loadingPhases = false;
  @observable loadingClasifications = false;
  @observable filteredPhases: IPhase[] = [];
  @observable filteredClasifications: IClasification[] = [];

  getPhase = (id: number): IPhase | undefined => {
    return this.filteredPhases.find((x) => x.id === id);
  };

  //   @action clearFilter = () => {
  //     this.filter = new PhaseFilterValues();
  //   };

  @action clearPhases = () => {
    this.phases = [];
  };

  @action clearClasifications = () => {
    this.clasifications = [];
  };

  @action setPhase = (id: number) => {
    let phase = this.getPhase(id);
    this.phase = phase;
  };

  //   @action setFilter = (e: any, result: any = null) => {
  //     const { name, value } = result || e.target;

  //     this.filter = {
  //       ...this.filter,
  //       [name]: value,
  //     };
  //   };

  //   @action index = async () => {
  //     this.rootStore.commonStore.loadingIndex = true;
  //     try {
  //       await Phase.index();
  //     } catch (error) {
  //       history.push("/forbidden");
  //       throw error;
  //     } finally {
  //       this.rootStore.commonStore.loadingIndex = false;
  //     }
  //   };

  @action get = async () => {
    this.loadingPhases = true;
    try {
      const phases = await Phase.get();
      this.phases = phases;
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.loadingPhases = false;
    }
  };

  @action getById = async (id: number) => {
    try {
      const phase = await Phase.getById(id);
      const index = this.phases.findIndex((x) => x.id === phase.id);
      if (index !== -1) this.phases[index] = phase;
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    }
  };

  @action getClasifications = async () => {
    this.loadingClasifications = true;
    try {
      const clasifications = await Phase.getClasifications(this.phase!.id);
      this.clasifications = clasifications;
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.loadingClasifications = false;
    }
  };

  @action filterPhases = (filterString: string = "") => {
    const keys: Array<keyof IPhase> = ["name"];
    this.filteredPhases = this.phases.filter(filterByText(keys, filterString));
  };

  @action filterClasifications = (filterString: string = "") => {
    const keys: Array<keyof IPhase> = ["name"];
    this.filteredClasifications = this.clasifications.filter(
      filterByText(keys, filterString)
    );
  };

  //   @action post = async (phase: IPhase) => {
  //     this.submitting = true;
  //     try {
  //       phase.id = await Phase.post(phase);
  //       phase.projectCount = 0;
  //       toast.success(Messages.postSuccess);
  //       this.phases.push(phase);
  //       this.rootStore.modalStore.closeModal();
  //     } catch (error) {
  //       if (error && error?.status !== 500) toast.error(getErrors(error));
  //     } finally {
  //       this.submitting = false;
  //     }
  //   };

  @action put = async (clasification: IClasification) => {
    try {
      await Phase.put(clasification);
      // toast.success(Messages.putSuccess);
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
      throw error;
    }
  };

  @action putPSP = async (phase: IPhase) => {
    try {
      await Phase.putPSP(phase);
      // toast.success(Messages.putSuccess);
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
      throw error;
    }
  };

  @action putAll = async (id: number) => {
    this.loadingClasifications = true;
    try {
      await Phase.putAll(id);
      toast.success(Messages.putSuccess);
      this.getClasifications();
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
      throw error;
    } finally {
      this.loadingClasifications = false;
    }
  };

  //   @action putEnabled = async (phase: IPhase) => {
  //     try {
  //       await Phase.put(phase);
  //       toast.success(Messages.putSuccess);
  //     } catch (error) {
  //       if (error && error?.status !== 500) toast.error(getErrors(error));
  //       throw error;
  //     }
  //   };

  //   @action download = async () => {
  //     this.loading = true;
  //     try {
  //       console.log("download");
  //       await Phase.download();
  //     } catch (error) {
  //       toast.error(getErrors(error));
  //     } finally {
  //       this.loading = false;
  //     }
  //   };
}
