import { ILogin, IUser } from "../models/user";
import requests from "./agent";

const User = {
  login: (login: ILogin): Promise<IUser> => requests.post("user/login", login),
};

export default User;