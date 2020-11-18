export interface IRole {
  id: number;
  name: string;
  active: boolean;
  protected: boolean;
}

export interface IRoleFilter {
  role: string;
  active: "all" | "yes" | "no";
}

export interface IRolePermission {
  menuId: number;
  roleId: number;
  name: string;
  access: boolean;
  create: boolean;
  update: boolean;
  delete: boolean;
}

export class RoleFormValues implements IRole {
  id: number = 0;
  name: string = "";
  active = false;
  protected = false;

  constructor(init?: IRole) {
    Object.assign(this, init);
  }
}
