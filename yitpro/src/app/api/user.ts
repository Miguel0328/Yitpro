import {
  IUser,
  IUserDetail,
  IUserFilter,
  IUserPermission,
} from "../models/user";
import requests from "./agent";

const User = {
  index: (): Promise<void> => requests.get("user/index"),
  get: (): Promise<IUser[]> => requests.get("user"),
  getFiltered: (filter: IUserFilter): Promise<IUser[]> =>
    requests.getBody("user/filter", filter),
  getById: (id: number): Promise<IUser> => requests.get("user", id),
  getDetail: (id?: number): Promise<IUserDetail> =>
    requests.get("user/detail", id),
  post: (user: FormData): Promise<number> => requests.postForm("user", user),
  put: (user: FormData): Promise<boolean> => requests.putForm("user", user),
  putEnabled: (user: IUser): Promise<boolean> =>
    requests.put("user/active", user),
  getPermissions: (id: number): Promise<IUserPermission[]> =>
    requests.get("user/permissions", id),
  putPermissions: (permissions: IUserPermission[]) =>
    requests.put("user/permissions", permissions),
  download: (): Promise<void> =>
    requests.download("user/download", "usuarios.xlsx"),
};

export default User;
