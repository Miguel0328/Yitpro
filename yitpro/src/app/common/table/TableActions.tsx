import React, { Fragment } from "react";
import { Container, Icon, Input } from "semantic-ui-react";

interface IProps {
  paginated: boolean;
  showColumn: boolean;
  showFilter: boolean;
  filterComponent?: JSX.Element;
  selected: number[];
  selectionActions?: JSX.Element;
  downloadAction?: () => Promise<void>;
  filterAction?: (filter?: string) => void;
  handlePrint: (() => void) | undefined;
  setShowFilter: React.Dispatch<React.SetStateAction<boolean>>;
  setShowColumn: React.Dispatch<React.SetStateAction<boolean>>;
}

const TableActions: React.FC<IProps> = ({
  paginated,
  showFilter,
  showColumn,
  filterComponent,
  selected,
  selectionActions,
  filterAction,
  downloadAction,
  handlePrint,
  setShowFilter,
  setShowColumn,
}) => {
  return (
    <th
      style={{ width: paginated === false ? "100%" : "50%" }}
      className="MuiTableCell-root MuiTableCell-head MuiTablePagination-root MuiTableCell-stickyHeader"
    >
      {selected.length > 0 && (
        <Fragment>
          <Container className="table-selected-actions" textAlign="left">
            <label>
              {selected.length}{" "}
              {selected.length === 1 ? "seleccionado" : "seleccionados"}
            </label>
          </Container>
          <Container className="table-selected-actions" textAlign="right">
            {selectionActions}
          </Container>
        </Fragment>
      )}
      {selected.length === 0 && (
        <Container fluid textAlign="right">
          {filterAction && (
            <Input
              size="small"
              icon="search"
              placeholder="Search..."
              className="search-table"
              onChange={(e) => {
                filterAction(e.target.value.toLowerCase());
              }}
            />
          )}
          {downloadAction && (
            <Icon
              className="icon-table-header"
              size="large"
              name="file excel"
              onClick={downloadAction}
            ></Icon>
          )}
          <Icon
            className="icon-table-header"
            size="large"
            name="print"
            onClick={handlePrint}
          ></Icon>
          {filterComponent && (
            <Icon
              className="icon-table-header"
              size="large"
              name="filter"
              onClick={() => {
                setShowFilter(!showFilter);
                setShowColumn(false);
              }}
            ></Icon>
          )}
          <Icon
            className="icon-table-header"
            size="large"
            name="columns"
            onClick={() => {
              setShowColumn(!showColumn);
              setShowFilter(false);
            }}
          ></Icon>
        </Container>
      )}
    </th>
  );
};

export default TableActions;
