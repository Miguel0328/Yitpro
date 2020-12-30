import { TableCell, TableHead, TableSortLabel } from "@material-ui/core";
import React from "react";
import { Checkbox, TableRow, CheckboxProps } from "semantic-ui-react";
import { IColumn, IShow, Order } from "../../models/table";

interface IProps {
  columns: IColumn[];
  headers?: IColumn[];
  paginated?: boolean;
  selectable?: boolean;
  order: Order;
  orderBy: string;
  orderable: boolean;
  displayedColumns: IShow[];
  numSelected: number;
  rowCount: number;
  onRequestSort: (event: React.MouseEvent<unknown>, property: string) => void;
  onSelectAll: (
    event: React.FormEvent<HTMLInputElement>,
    props: CheckboxProps
  ) => void;
}

const TableHeader: React.FC<IProps> = ({
  columns,
  headers,
  paginated,
  selectable,
  order,
  orderBy,
  orderable,
  displayedColumns,
  numSelected,
  rowCount,
  onRequestSort,
  onSelectAll,
}) => {
  const heads = headers ?? columns;
  const renderable = !!headers;

  const createSortHandler = (property: string) => (
    event: React.MouseEvent<unknown>
  ) => {
    onRequestSort(event, property);
  };

  return (
    <TableHead>
      <TableRow>
        {selectable && (
          <TableCell padding="checkbox">
            <Checkbox
              indeterminate={numSelected > 0 && numSelected < rowCount}
              checked={rowCount > 0 && numSelected === rowCount}
              onChange={onSelectAll}
            />
          </TableCell>
        )}
        {heads.map(
          (head) =>
            displayedColumns.find((x) => x.name === head.id)?.visible && (
              <TableCell
                key={head.id}
                align={head.align}
                style={{
                  top: paginated ? "46px" : "0px",
                  width: head.width,
                }}
                sortDirection={orderBy === head.id ? order : false}
              >
                {(head.orderable = head.orderable ?? true)}
                {orderable && head.orderable ? (
                  <TableSortLabel
                    active={orderBy === head.id}
                    direction={orderBy === head.id ? order : "asc"}
                    onClick={createSortHandler(head.id)}
                  >
                    {!head.render || !renderable ? head.label : head.render()}
                    {orderBy === head.id ? (
                      <span
                        style={{
                          border: 0,
                          clip: "rect(0 0 0 0)",
                          height: 1,
                          margin: -1,
                          overflow: "hidden",
                          padding: 0,
                          position: "absolute",
                          top: 20,
                          width: 1,
                        }}
                      >
                        {order === "desc"
                          ? "sorted descending"
                          : "sorted ascending"}
                      </span>
                    ) : null}
                  </TableSortLabel>
                ) : !head.render || !renderable ? (
                  head.label
                ) : (
                  head.render()
                )}
              </TableCell>
            )
        )}
      </TableRow>
    </TableHead>
  );
};

export default TableHeader;
