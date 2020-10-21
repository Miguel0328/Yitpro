import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  IconButton,
  Paper,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TablePagination,
  TableRow,
  useTheme,
  createStyles,
  makeStyles,
  Theme,
} from "@material-ui/core";
import React from "react";
import { Divider, Header, Segment } from "semantic-ui-react";
import FirstPageIcon from "@material-ui/icons/FirstPage";
import KeyboardArrowLeft from "@material-ui/icons/KeyboardArrowLeft";
import KeyboardArrowRight from "@material-ui/icons/KeyboardArrowRight";
import LastPageIcon from "@material-ui/icons/LastPage";

interface TablePaginationActionsProps {
  count: number;
  page: number;
  rowsPerPage: number;
  onChangePage: (
    event: React.MouseEvent<HTMLButtonElement>,
    newPage: number
  ) => void;
}

const useStyles1 = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      flexShrink: 0,
      marginLeft: theme.spacing(2.5),
    },
  })
);

const TablePaginationActions = (props: TablePaginationActionsProps) => {
  const classes = useStyles1();
  const theme = useTheme();
  const { count, page, rowsPerPage, onChangePage } = props;

  const handleFirstPageButtonClick = (
    event: React.MouseEvent<HTMLButtonElement>
  ) => {
    onChangePage(event, 0);
  };

  const handleBackButtonClick = (
    event: React.MouseEvent<HTMLButtonElement>
  ) => {
    onChangePage(event, page - 1);
  };

  const handleNextButtonClick = (
    event: React.MouseEvent<HTMLButtonElement>
  ) => {
    onChangePage(event, page + 1);
  };

  const handleLastPageButtonClick = (
    event: React.MouseEvent<HTMLButtonElement>
  ) => {
    onChangePage(event, Math.max(0, Math.ceil(count / rowsPerPage) - 1));
  };

  return (
    <div className={classes.root}>
      <IconButton
        onClick={handleFirstPageButtonClick}
        disabled={page === 0}
        aria-label="first page"
      >
        {theme.direction === "rtl" ? <LastPageIcon /> : <FirstPageIcon />}
      </IconButton>
      <IconButton
        onClick={handleBackButtonClick}
        disabled={page === 0}
        aria-label="previous page"
      >
        {theme.direction === "rtl" ? (
          <KeyboardArrowRight />
        ) : (
          <KeyboardArrowLeft />
        )}
      </IconButton>
      <IconButton
        onClick={handleNextButtonClick}
        disabled={page >= Math.ceil(count / rowsPerPage) - 1}
        aria-label="next page"
      >
        {theme.direction === "rtl" ? (
          <KeyboardArrowLeft />
        ) : (
          <KeyboardArrowRight />
        )}
      </IconButton>
      <IconButton
        onClick={handleLastPageButtonClick}
        disabled={page >= Math.ceil(count / rowsPerPage) - 1}
        aria-label="last page"
      >
        {theme.direction === "rtl" ? <FirstPageIcon /> : <LastPageIcon />}
      </IconButton>
    </div>
  );
};

interface IData {
  role: string;
  active: boolean;
  _protected: boolean;
}

const createData = (role: string, active: boolean, _protected: boolean) => {
  return { role, active, _protected };
};

const rows: { role: string; active: boolean; _protected: boolean }[] = [
  createData("Cupcake", true, false),
  createData("Donut", true, false),
  createData("Eclair", true, true),
  createData("Frozen yoghurt", false, false),
  createData("Gingerbread", true, false),
  createData("Honeycomb", true, false),
  createData("Ice cream sandwich", false, true),
  createData("Jelly Bean", true, false),
  createData("KitKat", false, false),
  createData("Lollipop", false, false),
  createData("Marshmallow", true, true),
  createData("Nougat", true, false),
  createData("Oreo", false, true),
].sort(
  (
    a: { role: string; active: boolean; _protected: boolean },
    b: { role: string; active: boolean; _protected: boolean }
  ) => (a.role < b.role ? -1 : 1)
);

const Role = () => {
  const [page, setPage] = React.useState(0);
  const [rowsPerPage, setRowsPerPage] = React.useState(5);

  const emptyRows =
    rowsPerPage - Math.min(rowsPerPage, rows.length - page * rowsPerPage);

  const handleChangePage = (
    event: React.MouseEvent<HTMLButtonElement> | null,
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
    <Segment style={{ minHeight: "100%" }}>
      <Header
        as="h2"
        icon={<FontAwesomeIcon icon="address-book" />}
        content="Roles"
      />
      <Divider section />
      <TableContainer component={Paper}>
        <Table aria-label="custom table">
          <TableHead>
            <TableRow>
              <TablePagination
                rowsPerPageOptions={[5, 10, 25, { label: "All", value: -1 }]}
                colSpan={5}
                count={rows.length}
                page={page}
                rowsPerPage={rowsPerPage}
                onChangePage={handleChangePage}
                onChangeRowsPerPage={handleChangeRowsPerPage}
                ActionsComponent={TablePaginationActions}
              />
            </TableRow>
          </TableHead>
          <TableHead style={{ backgroundColor: "lightgray", height: "20" }}>
            <TableRow>
              <TableCell align="center">Role</TableCell>
              <TableCell align="center">Active</TableCell>
              <TableCell align="center">Protected</TableCell>
              <TableCell align="center">Permisssion</TableCell>
              <TableCell align="center">Edit</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {rows.length === 0 ? (
              <TableRow>
                <TableCell
                  component="th"
                  colSpan={5}
                  align="center"
                  scope="row"
                >
                  No data to show
                </TableCell>
              </TableRow>
            ) : (
              (rowsPerPage > 0
                ? rows.slice(
                    page * rowsPerPage,
                    page * rowsPerPage + rowsPerPage
                  )
                : rows
              ).map((row) => (
                <TableRow key={row.role}>
                  <TableCell component="th" scope="row">
                    {row.role}
                  </TableCell>
                  <TableCell style={{ width: 100 }} align="center">
                    {row.active ? "Yes" : "No"}
                  </TableCell>
                  <TableCell style={{ width: 100 }} align="center">
                    {row._protected ? "Yes" : "No"}
                  </TableCell>
                  <TableCell style={{ width: 100 }} align="center">
                    <FontAwesomeIcon className="table" size="lg" icon="lock" />
                  </TableCell>
                  <TableCell style={{ width: 100 }} align="center">
                    <FontAwesomeIcon className="table" size="lg" icon="edit" />
                  </TableCell>
                </TableRow>
              ))
            )}
          </TableBody>
        </Table>
      </TableContainer>
    </Segment>
  );
};

export default Role;
