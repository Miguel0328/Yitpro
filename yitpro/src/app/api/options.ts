import { IOption } from "../models/common";
import requests from "./agent";

const Option = {
  getRoles: (): Promise<IOption[]> => requests.get("option/roles"),
  getManagers: (): Promise<IOption[]> => requests.get("option/managers"),
  getClients: (): Promise<IOption[]> => requests.get("option/clients"),
  getCatalogs: (id?: number): Promise<IOption[]> =>
    requests.get("option/catalogs", id),
};

export default Option;
