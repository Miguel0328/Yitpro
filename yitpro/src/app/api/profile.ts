import { ILogin, IProfile } from "../models/profile";
import requests from "./agent";

const Profile = {
  login: (login: ILogin): Promise<IProfile> => requests.post("profile/login", login),
  current: (): Promise<IProfile> => requests.get("profile")
};

export default Profile;