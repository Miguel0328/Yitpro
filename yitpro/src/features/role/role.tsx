import { observer } from "mobx-react-lite";
import React, { useContext, useEffect } from "react";
import { Divider, Segment } from "semantic-ui-react";
import { RootStoreContext } from "../../app/stores/rootStore";
import RoleHeader from "./RoleHeader";
import RoleTable from "./RoleTable";

const Role = () => {
  const rootStore = useContext(RootStoreContext);
  const { index, getRoles, clearRoles } = rootStore.roleStore;
  const { loadingIndex } = rootStore.commonStore;

  useEffect(() => {
    index()
      .then(getRoles)
      .catch((error) => console.log(error));

    return clearRoles();
  }, [index, getRoles, clearRoles]);

  if (loadingIndex) return null;

  return (
    <Segment loading={loadingIndex} className="principal-segment">
      <RoleHeader />
      <Divider section className="principal-divider" />
      <RoleTable />
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

export default observer(Role);
