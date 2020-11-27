import { IRole, IRolePermission } from "../models/role";
import requests from "./agent";

const Role = {
  index: (): Promise<void> => requests.get("role/index"),
  get: (id?: number): Promise<IRole[]> => requests.get("role", id),
  post: (role: IRole): Promise<boolean> => requests.post("role", role),
  put: (role: IRole): Promise<boolean> => requests.put("role", role),
  putEnable: (role: IRole): Promise<boolean> => requests.put("role", role),
  getPermissions: (id: number): Promise<IRolePermission[]> =>
    requests.get("role/permissions", id),
  putPermissions: (permissions: IRolePermission[]) =>
    requests.put("role/permissions", permissions),
};

export default Role;
