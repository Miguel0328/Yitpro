export interface IUser {
  id: number;
  name: string;
  email: string;
  role: string;
  department: string;
  photo: string;
  active: boolean;
}

export interface IUserFilter {
  name: string;
  email: string;
  roleId?: number | "";
  active?: boolean | "";
}

export interface IUserDetail {
  id: number;
  employeeNumber: string;
  firstName: string;
  lastName: string;
  secondLastName: string;
  email: string;
  roleId: number | "";
  departmentId: number | "";
  managerId?: number;
  admissionDate?: Date;
  photoUrl: string;
  capture: boolean;
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

export class UserFilterValues implements IUserFilter {
  name = "";
  email = "";
  roleId?: number | "" = "";
  active?: boolean | "" = "";

  constructor(init?: IUserFilter) {
    Object.assign(this, init);
  }
}

export class UserFormValues implements IUserDetail {
  id: number = 0;
  employeeNumber = "";
  firstName = "";
  lastName = "";
  secondLastName = "";
  email = "";
  roleId: number | "" = "";
  departmentId: number | "" = "";
  admissionDate?: Date = undefined;
  photoUrl = "/assets/avatar.png";
  active = true;
  capture = false;
  locked = false;

  constructor(init?: IUserDetail) {
    Object.assign(this, init);
  }
}
