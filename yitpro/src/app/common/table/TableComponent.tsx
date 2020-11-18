import React from "react";
import Paper from "@material-ui/core/Paper";
import Table from "@material-ui/core/Table";
import TableBody from "@material-ui/core/TableBody";
import TableCell from "@material-ui/core/TableCell";
import TableContainer from "@material-ui/core/TableContainer";
import TableHead from "@material-ui/core/TableHead";
import TablePagination from "@material-ui/core/TablePagination";
import TableRow from "@material-ui/core/TableRow";
import { observer } from "mobx-react-lite";
import TablePaginationActions from "./TablePaginationActions";
import { v4 as uuid } from "uuid";
import { IColumn } from "../../models/table";

interface IProps {
  data: any[];
  columns: IColumn[];
  paginated?: boolean;
  headers?: IColumn[];
}

const TableComponent: React.FC<IProps> = ({
  data,
  columns,
  paginated = true,
  headers = undefined,
}) => {
  const [page, setPage] = React.useState(0);
  const [rowsPerPage, setRowsPerPage] = React.useState(
    paginated ? 10 : data.length
  );

  const handleChangePage = (
    _event: React.MouseEvent<HTMLButtonElement> | null,
    newPage: number
  ) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (
    event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    setRowsPerPage(parseInt(event.target.value, 10));
    setPage(0);
  };

  return (
    <TableContainer component={Paper} style={{ maxHeight: 500 }}>
      <Table stickyHeader aria-label="sticky table">
        {paginated && (
          <TableHead key="pagination">
            <TableRow>
              <TablePagination
                rowsPerPageOptions={[10, 25, 100, { label: "All", value: -1 }]}
                colSpan={5}
                count={data.length}
                page={page}
                rowsPerPage={rowsPerPage}
                onChangePage={handleChangePage}
                onChangeRowsPerPage={handleChangeRowsPerPage}
                ActionsComponent={TablePaginationActions}
              />
            </TableRow>
          </TableHead>
        )}
        <TableHead key="header">
          <TableRow>
            {!headers
              ? columns.map((column) => (
                  <TableCell
                    key={column.id}
                    align="center"
                    style={{
                      top: paginated ? "46px" : "0px",
                      width: column.width,
                    }}
                  >
                    {column.label}
                  </TableCell>
                ))
              : headers.map((header) => (
                  <TableCell
                    key={header.id}
                    align="center"
                    style={{
                      top: paginated ? "46px" : "0px",
                      width: header.width,
                    }}
                  >
                    {!header.render ? header.label : header.render()}
                  </TableCell>
                ))}
          </TableRow>
        </TableHead>
        <TableBody>
          {data.length === 0 ? (
            <TableRow>
              <TableCell component="th" colSpan={5} align="center" scope="role">
                No data to show
              </TableCell>
            </TableRow>
          ) : (
            (rowsPerPage > 0
              ? data.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
              : data
            ).map((datum) => (
              <TableRow
                hover
                role="checkbox"
                tabIndex={-1}
                key={uuid()}
              >
                {columns.map((column) => {
                  return (
                    <TableCell
                      key={uuid()}
                      align={column.align}
                    >
                      {column.render === undefined
                        ? datum[column.id]
                        : column.render(datum)}
                    </TableCell>
                  );
                })}
              </TableRow>
            ))
          )}
        </TableBody>
      </Table>
    </TableContainer>
  );
};

export default observer(TableComponent);
