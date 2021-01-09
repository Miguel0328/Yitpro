import { AxiosResponse } from "axios";
import { Messages } from "../../models/messages";
import { Order } from "../../models/table";

export const getErrors = (error: AxiosResponse) => {
  try {
    return `${error.statusText}\n
  ${
    !error.data && error.status === 403
      ? Messages.forbidden
      : error.data && error.status === 500
      ? error.data.errors
      : (error.data.errors &&
          Object.keys(error.data.errors).length > 0 &&
          Object.values(error.data.errors)
            .flat()
            .map((err: any) => err + "\n")
            .join("")) ||
        ""
  }`;
  } catch (e) {
    console.log(error);
    console.log(e);
    return `Error de sistema`;
  }
};

export function descendingComparator<T>(a: T, b: T, orderBy: keyof T) {
  let bl: any;
  let al: any;

  if (orderBy.toString().includes(".")) {
    const parent: any = orderBy.toString().split(".")[0];
    const child: any = orderBy.toString().split(".")[1];
    const aT: any = a;
    const bT: any = b;

    bl = bT[parent][child];
    al = aT[parent][child];
  } else {
    bl = b[orderBy];
    al = a[orderBy];
  }

  bl = typeof bl === "string" ? bl.toLowerCase() : bl;
  al = typeof al === "string" ? al.toLowerCase() : al;

  if (bl < al) {
    return -1;
  }
  if (bl > al) {
    return 1;
  }

  return 0;
}

export function getComparator<Key extends keyof any>(
  order: Order,
  orderBy: Key
): (
  a: { [key in Key]: number | string },
  b: { [key in Key]: number | string }
) => number {
  return order === "desc"
    ? (a, b) => descendingComparator(a, b, orderBy)
    : (a, b) => -descendingComparator(a, b, orderBy);
}

export function stableSort<T>(array: T[], comparator: (a: T, b: T) => number) {
  const stabilizedThis = array.map((el, index) => [el, index] as [T, number]);
  stabilizedThis.sort((a, b) => {
    const order = comparator(a[0], b[0]);
    if (order !== 0) return order;
    return a[1] - b[1];
  });
  return stabilizedThis.map((el) => el[0]);
}

// eslint-disable-next-line no-extend-native
export const filterByText = (keys: string[], searchFilter: string) => (
  el: any
) => {
  return keys.some((key) => {
    let value = "";
    if (typeof el[key] === "boolean") {
      value = el[key] ? "Sí" : "No";
    } else {
      value = el[key].toString();
    }
    if (
      (el[key] !== null && value.toLowerCase().includes(searchFilter)) ||
      searchFilter === ""
    )
      return true;
    else return false;
  });
};
