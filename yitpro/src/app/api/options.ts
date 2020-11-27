import { IOption } from "../models/options";
import requests from "./agent";

const Option = {
  getRoles: (id?: number): Promise<IOption[]> => requests.get("option/roles", id),
  getLineManagers: (id?: number): Promise<IOption[]> => requests.get("option/line-managers", id),
};

export default Option;
