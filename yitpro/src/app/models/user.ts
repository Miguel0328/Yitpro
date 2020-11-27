export interface IUser {
  id: number;
  name: string;
  email: string;
  role: string;
  roleId: number;
  photo: string;
  active: boolean;
}

export interface IUserDetails {
  id: number;
  employeeNumber: string;
  firstName: string;
  lastName: string;
  secondLastName: string;
  email: string;
  roleId: number | "";
  managerId?: number;
  admissionDate?: Date;
  photoUrl: string;
  active: boolean;
  locked: boolean;
}

export interface IUserPermission {
  menuId: number;
  userId: number;
  name: string;
  icon: string;
  level: number;
  access: boolean;
  create: boolean;
  update: boolean;
  delete: boolean;
}

export interface IUserFilter {
  user: string;
  email: string;
  active: string;
  roleId: number | "";
}

export class UserFormValues implements IUserDetails {
  id: number = 0;
  employeeNumber = "";
  firstName = "";
  lastName = "";
  secondLastName = "";
  email = "";
  roleId: number | "" = "";
  admissionDate?: Date = undefined;
  photoUrl = "/assets/avatar.png";
  active = true;
  locked = false;

  constructor(init?: IUserDetails) {
    Object.assign(this, init);
  }
}
