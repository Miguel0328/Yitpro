export interface IClient {
    id: number;
    name: string;
    projectCount?: number;
    active: boolean;
  }
  
  export interface IClientFilter {
    client: string;
    active: "" | "yes" | "no";
  }
  
  export class ClientFormValues implements IClient {
    id = 0;
    name = "";
    active = true;
  
    constructor(init?: IClient) {
      Object.assign(this, init);
    }
  }
  