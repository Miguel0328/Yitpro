import { IOption } from "../models/common";
import requests from "./agent";

const Option = {
  getRoles: (): Promise<IOption[]> => requests.get("option/roles"),
  getManagers: (): Promise<IOption[]> => requests.get("option/managers"),
  getResponsibles: (): Promise<IOption[]> =>
    requests.get("option/responsibles"),
  getProjects: (): Promise<IOption[]> => requests.get("option/projects"),
  getClients: (): Promise<IOption[]> => requests.get("option/clients"),
  getProjectTeam: (id: number): Promise<IOption[]> =>
    requests.get("option/project/team", id),
  getClasifications: (id: number): Promise<IOption[]> =>
    requests.get("option/clasifications", id),
  getCatalogs: (id?: number): Promise<IOption[]> =>
    requests.get("option/catalogs", id),
};

export default Option;
