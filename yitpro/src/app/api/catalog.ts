import { ICatalog } from "../models/catalog";
import requests from "./agent";

const Catalog = {
  index: (): Promise<void> => requests.get("catalog/index"),
  get: (id: number): Promise<ICatalog[]> => requests.get("catalog", id),
  getDetail: (id: number): Promise<ICatalog[]> =>
    requests.get("catalog/detail", id),
  post: (catalog: ICatalog): Promise<number> =>
    requests.post("catalog", catalog),
  put: (catalog: ICatalog): Promise<boolean> =>
    requests.put("catalog", catalog),
  putEnable: (catalog: ICatalog): Promise<boolean> =>
    requests.put("catalog", catalog),
  download: (): Promise<void> =>
    requests.download("catalog/download", "catalogos.xlsx"),
};

export default Catalog;
