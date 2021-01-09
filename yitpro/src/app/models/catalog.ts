export class GC {
  static projectPhase: number = 2;
  static department: number = 3;
  static projectMethodology: number = 4;
  static projectType: number = 5;
  static projectStatus: number = 6;
}

export interface ICatalog {
  id: number;
  catalogId: number;
  alias: string;
  description: string;
  active: boolean;
  protected: boolean;
}

export interface ICatalogFilter {
  alias: string;
  description: string;
  active: "" | "yes" | "no";
  protected: "" | "yes" | "no";
}

export class CatalogFilterValues implements ICatalogFilter {
  alias = "";
  description = "";
  active: "" | "yes" | "no" = "";
  protected: "" | "yes" | "no" = "";

  constructor(init?: ICatalog) {
    Object.assign(this, init);
  }
}

export class CatalogFormValues implements ICatalog {
  id = 0;
  catalogId = 0;
  alias = "";
  description = "";
  active = true;
  protected = false;

  constructor(init?: ICatalog) {
    Object.assign(this, init);
  }
}
