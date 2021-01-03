export interface IColumn {
  id: string;
  render?: any;
  label: string;
  width: string;
  align: "left" | "center" | "right";
  style?: (obj?: any) => object;
  orderable?: boolean;
}

export type Order = "asc" | "desc";

export interface IShow {
  name: string;
  visible: boolean;
}
