// import { ISelected } from "../models/common";
import {
  IActivity,
  IActivityDetail,
  //   IActivityFilter,
  //   IActivityTeam,
} from "../models/activity";
// import { IUser } from "../models/user";
import requests from "./agent";

const Activity = {
  //   index: (): Promise<void> => requests.get("activity/index"),
  //   indexDetial: (id: number): Promise<void> =>
  //     requests.get("activity/index/detail", id),
  get: (): Promise<IActivity[]> => requests.get("activity"),
  //   getId: (code: string): Promise<number> =>
  //     requests.get("activity/get-id", code),
  //   getFiltered: (filter: IActivityFilter): Promise<IActivity[]> =>
  //     requests.getBody("activity/filter", filter),
  // getById: (id: number): Promise<IActivity> =>
  //   requests.get("activity", id),
    getDetail: (id: number): Promise<IActivityDetail> =>
      requests.get("activity/detail", id),
  //   getTeam: (id: number): Promise<IActivityTeam[]> =>
  //     requests.get("activity/team", id),
  //   getRemainingTeam: (id: number): Promise<IUser[]> =>
  //     requests.get("activity/team/remaining", id),
  post: (activity: IActivityDetail): Promise<number> =>
    requests.post("activity", activity),
  //   postTeam: (team: ISelected, id: number): Promise<boolean> =>
  //     requests.post("activity/team", team, id),
  //   put: (activity: IActivityDetail, id: number): Promise<boolean> =>
  //     requests.put("activity", activity, id),
  //   putEnabled: (activity: IActivity, id: number): Promise<boolean> =>
  //     requests.put("activity/active", activity, id),
  //   deleteTeam: (team: ISelected, id: number): Promise<boolean> =>
  //     requests.put("activity/team", team, id),
  //   download: (): Promise<void> =>
  //     requests.download("activity/download", "proyectos.xlsx"),
  //   downloadTeam: (id: number): Promise<void> =>
  //     requests.download("activity/team/download", "equipo.xlsx", id),
};

export default Activity;
