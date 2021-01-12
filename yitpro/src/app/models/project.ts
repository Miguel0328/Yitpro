export class ProjectStatus {
  static canceled = "Cancelado";
}

export interface IProject {
  id: number;
  client: string;
  name: string;
  code: string;
  leader: string;
  leaderPhoto: string;
  type: string;
  methodology: string;
  status: string;
  active: boolean;
}

export interface IProjectTeam {
  id: number;
  user: string;
  userPhoto: string;
}

export interface IProjectFilter {
  name: string;
  code: string;
  clientId?: number | "";
  active?: boolean | "";
}

export interface IProjectDetail {
  id: number;
  clientId: number | "";
  name: string;
  code: string;
  leaderId: number | "";
  typeId: number | "";
  statusId: number | "";
  methodologyId: number | "";
  description: string;
  active: boolean;
  psp: boolean;
}

export class ProjectFilterValues implements IProjectFilter {
  name: string = "";
  code: string = "";
  clientId?: number | "" = "";
  active?: boolean | "" = "";

  constructor(init?: IProjectFilter) {
    Object.assign(this, init);
  }
}

export class ProjectFormValues implements IProjectDetail {
  id: number = 0;
  clientId: number | "" = "";
  name: string = "";
  code: string = "";
  leaderId: number | "" = "";
  typeId: number | "" = "";
  statusId: number | "" = "";
  methodologyId: number | "" = "";
  description: string = "";
  active: boolean = true;
  psp: boolean = false;

  constructor(init?: IProjectDetail) {
    Object.assign(this, init);
  }
}
