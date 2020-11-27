import { IUser, IUserDetails, IUserPermission } from "../models/user";
import requests from "./agent";

const User = {
  index: (): Promise<void> => requests.get("user/index"),
  get: (): Promise<IUser[]> => requests.get("user"),
  getById: (id?: number): Promise<IUserDetails> => requests.get("user", id),
  post: (user: FormData): Promise<boolean> => requests.postForm("user", user),
  put: (user: FormData): Promise<boolean> => requests.putForm("user", user),
  putEnabled: (user: IUser): Promise<boolean> => requests.put("user/active", user),
  getPermissions: (id: number): Promise<IUserPermission[]> =>
    requests.get("user/permissions", id),
  putPermissions: (permissions: IUserPermission[]) =>
    requests.put("user/permissions", permissions),
};

export default User;
