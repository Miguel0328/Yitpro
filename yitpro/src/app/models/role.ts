export interface IRole {
  id: number;
  name: string;
  active: boolean;
  protected: boolean;
}

export interface IRoleFilter {
  role: string;
  active: "" | "yes" | "no";
  protected: "" | "yes" | "no";
}

export interface IRolePermission {
  menuId: number;
  roleId: number;
  name: string;
  icon: string;
  level: number;
  access: boolean;
  create: boolean;
  update: boolean;
  delete: boolean;
}

export class RoleFilterValues implements IRoleFilter {
  role = "";
  active: "" | "yes" | "no" = "";
  protected: "" | "yes" | "no" = "";

  constructor(init?: IRole) {
    Object.assign(this, init);
  }
}

export class RoleFormValues implements IRole {
  id = 0;
  name = "";
  active = true;
  protected = false;

  constructor(init?: IRole) {
    Object.assign(this, init);
  }
}
