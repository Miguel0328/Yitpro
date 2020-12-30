import { IClient } from "../models/client";
import requests from "./agent";

const Client = {
  index: (): Promise<void> => requests.get("client/index"),
  get: (): Promise<IClient[]> => requests.get("client"),
  post: (client: IClient): Promise<number> => requests.post("client", client),
  put: (client: IClient): Promise<boolean> => requests.put("client", client),
  putEnable: (client: IClient): Promise<boolean> => requests.put("client", client),
  download: (): Promise<void> =>
    requests.download("client/download", "clients.xlsx"),
};

export default Client;
