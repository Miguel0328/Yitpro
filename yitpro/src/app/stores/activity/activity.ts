import { observable, action, reaction } from "mobx";
import { toast } from "react-toastify";
import { history } from "../../..";
// import Activity from "../../api/activity";
import { filterByText, getErrors } from "../../common/util/util";
import { Messages } from "../../models/messages";
import {
  IActivity,
  IActivityDetail,
  //   IActivityFilter,
  //   ActivityFilterValues,
} from "../../models/activity";
import { RootStore } from "../root";
import Activity from "../../api/activity";

export default class ActivityStore {
  rootStore: RootStore;

  constructor(rootStore: RootStore) {
    this.rootStore = rootStore;

    reaction(
      () => this.activities.slice(),
      () => {
        this.filtered = this.activities;
      }
    );
  }

  @observable activity: IActivityDetail | undefined;
  @observable activityId: number = 0;
  //   @observable activityCode: string = "";
  //   @observable activityName: string = "";
  @observable activities: IActivity[] = [];
  //   @observable filter: IActivityFilter = new ActivityFilterValues();
  @observable loading = false;
  //   @observable loadingDetail = false;
  @observable submitting = false;
  @observable filtered: IActivity[] = [];

  //   @action clearFilter = () => {
  //     this.filter = new ActivityFilterValues();
  //   };

  @action clearActivities = () => {
    this.activities = [];
  };

  @action clearActivity = () => {
    this.activity = undefined;
  };

  @action initForm = async () => {
    this.rootStore.optionStore.getProjectOptions();
    this.rootStore.optionStore.getResponsibleOptions();
    this.rootStore.optionStore.getProjectPhaseOptions();
  };

  //   @action setActivityCode = (code: string) => {
  //     this.activityCode = code;
  //   };

  @action setActivityId = (id: number) => {
    this.activityId = id;
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
  //       await Activity.index();
  //     } catch (error) {
  //       history.push("/forbidden");
  //       throw error;
  //     } finally {
  //       this.rootStore.commonStore.loadingIndex = false;
  //     }
  //   };

  //   @action indexDetail = async () => {
  //     this.rootStore.commonStore.loadingIndex = true;
  //     try {
  //       await Activity.indexDetial(this.activityId);
  //     } catch (error) {
  //       history.push("/forbidden");
  //       throw error;
  //     } finally {
  //       this.rootStore.commonStore.loadingIndex = false;
  //     }
  //   };

  @action get = async () => {
    this.loading = true;
    try {
      const activities = await Activity.get();
      this.activities = activities;
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.loading = false;
    }
  };

  @action getDetail = async () => {
    try {
      this.submitting = true;
      const activity = await Activity.getDetail(this.activityId);
      activity.period = [
        new Date(activity.period[0]),
        new Date(activity.period[1]),
      ];
      activity.justRead = true;
      this.activity = activity;
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.submitting = false;
    }
  };

  //   @action filterActivities = async () => {
  //     this.loading = true;
  //     try {
  //       this.filter.active =
  //         this.filter.active === "" ? undefined : this.filter.active;
  //       this.filter.clientId =
  //         this.filter.clientId === "" ? undefined : this.filter.clientId;
  //       const activities = await Activity.getFiltered(this.filter);
  //       this.activities = activities;
  //     } catch (error) {
  //       if (error && error?.status !== 500) toast.error(getErrors(error));
  //     } finally {
  //       this.loading = false;
  //     }
  //   };

  //   @action filterByText = (filterString: string = "") => {
  //     const keys: Array<keyof IActivity> = ["name", "code", "leader", "client"];
  //     this.filtered = this.activities.filter(filterByText(keys, filterString));
  //   };

  @action post = async (activity: IActivityDetail) => {
    this.submitting = true;
    try {
      const id = await Activity.post(activity);
      toast.success(Messages.postSuccess);
      // const newActivity = await Activity.getById(id);
      // this.activities.push(newActivity);
      this.rootStore.modalStore.closeModal();
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.submitting = false;
    }
  };

  //   @action put = async (activity: IActivityDetail) => {
  //     this.submitting = true;
  //     try {
  //       await Activity.put(activity, activity.id);
  //       toast.success(Messages.putSuccess);
  //       const updatedActivity = await Activity.getById(activity.id);
  //       const index = this.activities.findIndex(
  //         (x) => x.id === updatedActivity.id
  //       );
  //       if (index !== -1) this.activities[index] = updatedActivity;
  //       this.rootStore.modalStore.closeModal();
  //     } catch (error) {
  //       if (error && error?.status !== 500) toast.error(getErrors(error));
  //     } finally {
  //       this.submitting = false;
  //     }
  //   };

  //   @action putEnabled = async (activity: IActivity) => {
  //     try {
  //       await Activity.putEnabled(activity, activity.id);
  //       toast.success(Messages.putSuccess);
  //     } catch (error) {
  //       if (error && error?.status !== 500) toast.error(getErrors(error));
  //       throw error;
  //     }
  //   };

  //   @action download = async () => {
  //     this.loading = true;
  //     try {
  //       await Activity.download();
  //     } catch (error) {
  //       toast.error(getErrors(error));
  //     } finally {
  //       this.loading = false;
  //     }
  //   };
}
