import {
  IProject,
  IProjectDetail,
  IProjectFilter,
  IProjectTeam,
} from "../models/project";
import requests from "./agent";

const Project = {
  index: (): Promise<void> => requests.get("project/index"),
  get: (): Promise<IProject[]> => requests.get("project"),
  getId: (code: string): Promise<number> =>
    requests.get("project/get-id", code),
  getFiltered: (filter: IProjectFilter): Promise<IProject[]> =>
    requests.getBody("project/filter", filter),
  getById: (id: number): Promise<IProject> => requests.get("project", id),
  getDetail: (id?: number): Promise<IProjectDetail> =>
    requests.get("project/detail", id),
  getTeam: (id: number): Promise<IProjectTeam[]> =>
    requests.get("project/team", id),
  post: (project: IProjectDetail): Promise<number> =>
    requests.post("project", project),
  put: (project: IProjectDetail): Promise<boolean> =>
    requests.put("project", project),
  putEnabled: (project: IProject): Promise<boolean> =>
    requests.put("project/active", project),
  download: (): Promise<void> =>
    requests.download("project/download", "proyectos.xlsx"),
};

export default Project;
