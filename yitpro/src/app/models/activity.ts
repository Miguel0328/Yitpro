import { IUser } from "./user";

export interface IActivity {
  id: number;
  activity: string;
  status: string;
  description: string;
  phase: string;
  assigned: IUser;
  responsible: IUser;
  assignedTime: number;
  finalTime: number;
  urgent: boolean;
  critical: boolean;
  endDate: Date;
  finalDate?: Date;
}

export interface IActivityDetail {
  id: number;
  projectId: number | "";
  assignedId: number | "";
  phaseId: number | "";
  clasificationId: number | "";
  responsibleId: number | "";
  assignedTime: string;
  estimatedTime: string;
  period: Date[];
  requirement: string;
  description: string;
  critical: boolean;
  planned: boolean;
  urgent: boolean;
  finalDate?: Date;
  justRead: boolean;
}

export class ActivityFormValues implements IActivityDetail {
  id = 0;
  projectId: number | "" = "";
  assignedId: number | "" = "";
  phaseId: number | "" = "";
  clasificationId: number | "" = "";
  responsibleId: number | "" = "";
  estimatedTime = "";
  assignedTime = "";
  period: Date[] = [];
  requirement = "";
  description = "";
  critical = false;
  planned = false;
  urgent = false;
  finalDate?: Date = undefined;
  justRead = false;

  constructor(init?: IActivityDetail) {
    Object.assign(this, init);
  }
}
