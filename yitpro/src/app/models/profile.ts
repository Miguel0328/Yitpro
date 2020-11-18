export interface IProfile {
  name: string;
  token: string;
  menus: IMenu[];
}

export interface IMenu {
  menuId: number;
  description: string;
  route: string;
  level: number;
  icon: string;
  submenus: IMenu[];
}

export interface ILogin {
  email: string;
  password: string;
}
