import { IOption } from "../models/options";
import requests from "./agent";

const Option = {
  getRoles: (): Promise<IOption[]> => requests.get("option/roles"),
  getLineManagers: (): Promise<IOption[]> => requests.get("option/line-managers"),
  getClients: (): Promise<IOption[]> => requests.get("option/clients"),
};

export default Option;
