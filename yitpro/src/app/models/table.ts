export interface IColumn {
  id: string;
  render?: any;
  label: string;
  width: string;
  align: "left" | "center" | "right";
}
