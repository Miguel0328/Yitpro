import { ISelected } from "../models/common";
import {
  IProject,
  IProjectDetail,
  IProjectFilter,
  IProjectTeam,
} from "../models/project";
import { IUser } from "../models/user";
import requests from "./agent";

const Project = {
  index: (): Promise<void> => requests.get("project/index"),
  indexDetial: (id: number): Promise<void> =>
    requests.get("project/index/detail", id),
  get: (): Promise<IProject[]> => requests.get("project"),
  getId: (code: string): Promise<number> =>
    requests.get("project/get-id", code),
  getFiltered: (filter: IProjectFilter): Promise<IProject[]> =>
    requests.getBody("project/filter", filter),
  getById: (id: number): Promise<IProject> => requests.get("project", id),
  getDetail: (id: number): Promise<IProjectDetail> =>
    requests.get("project/detail", id),
  getTeam: (id: number): Promise<IProjectTeam[]> =>
    requests.get("project/team", id),
  getRemainingTeam: (id: number): Promise<IUser[]> =>
    requests.get("project/team/remaining", id),
  post: (project: IProjectDetail): Promise<number> =>
    requests.post("project", project),
  postTeam: (team: ISelected, id: number): Promise<boolean> =>
    requests.post("project/team", team, id),
  put: (project: IProjectDetail, id: number): Promise<boolean> =>
    requests.put("project", project, id),
  putEnabled: (project: IProject, id: number): Promise<boolean> =>
    requests.put("project/active", project, id),
  deleteTeam: (team: ISelected, id: number): Promise<boolean> =>
    requests.put("project/team", team, id),
  download: (): Promise<void> =>
    requests.download("project/download", "proyectos.xlsx"),
  downloadTeam: (id: number): Promise<void> =>
    requests.download("project/team/download", "equipo.xlsx", id),
};

export default Project;
