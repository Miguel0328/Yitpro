import React, { ChangeEvent, Fragment, useState } from "react";
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
import { IColumn, IShow, Order } from "../../models/table";
import TableHeader from "./TableHeader";
import { stableSort, getComparator } from "../../common/util/util";
import {
  Checkbox,
  CheckboxProps,
  Dimmer,
  Loader,
  Segment,
} from "semantic-ui-react";
import { useReactToPrint } from "react-to-print";
import TableActions from "./TableActions";
import TableColumns from "./TableColumns";

interface IProps {
  data: any[];
  columns: IColumn[];
  headers?: IColumn[];
  header?: boolean;
  paginated?: boolean;
  selectable?: boolean;
  actions?: boolean;
  orderable?: boolean;
  orderColumn?: string;
  orderDirection?: string;
  filterComponent?: JSX.Element;
  selectionActions?: JSX.Element;
  selected?: number[];
  setSelected?: (selected: number[]) => void;
  filterAction?: (filter?: string) => void;
  downloadAction?: () => Promise<void>;
}

const TableComponent: React.FC<IProps> = ({
  data,
  columns,
  headers,
  header = true,
  paginated = true,
  selectable = false,
  actions = true,
  orderable = true,
  orderDirection = "asc",
  orderColumn = "",
  filterComponent,
  selectionActions,
  selected,
  setSelected,
  filterAction,
  downloadAction,
}) => {
  const tableRef = React.useRef<HTMLElement | null>(null);
  const onBeforeGetContentResolve = React.useRef<(() => void) | null>(null);
  const displayedColumns: IShow[] = [];
  const [visibleColumns, setVisibleColum] = useState<IShow[]>(displayedColumns);
  const [page, setPage] = React.useState(0);
  const [rowsPerPage, setRowsPerPage] = React.useState(
    paginated ? 10 : data.length
  );
  const [order, setOrder] = React.useState<Order>(orderDirection as Order);
  const [orderBy, setOrderBy] = React.useState<string>(orderColumn);
  const [loadingPrintHeight, setLoadingPrintHeight] = useState<number>(0);
  const [loadingPrint, setLoadingPrint] = React.useState(false);
  const [showFilter, setShowFilter] = useState(false);
  const [showColumn, setShowColumn] = useState(false);
  // const [selected, setSelected] = useState<number[]>([]);

  const handleSelect = (id: number) => {
    selected = selected!;
    const index = selected.indexOf(id);
    let selectedArray: number[] = [];

    if (index === -1) {
      selectedArray = selectedArray.concat(selected, id);
    } else if (index === 0) {
      selectedArray = selectedArray.concat(selected.slice(1));
    } else if (index === selected.length - 1) {
      selectedArray = selectedArray.concat(selected.slice(0, -1));
    } else if (index > 0) {
      selectedArray = selectedArray.concat(
        selected.slice(0, index),
        selected.slice(index + 1)
      );
    }

    if (setSelected) setSelected(selectedArray);
  };

  const handleSelectAll = (
    event: React.FormEvent<HTMLInputElement>,
    props: CheckboxProps
  ) => {
    if (props.checked) {
      const allSelected = data.map((n: any) => n.id);
      setSelected!(allSelected);
      return;
    }
    setSelected!([]);
  };

  const isSelected = (id: number) => selected!.indexOf(id) !== -1;

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

  const handleRequestSort = (
    event: React.MouseEvent<unknown>,
    property: string
  ) => {
    const isAsc = orderBy === property && order === "asc";
    setOrder(isAsc ? "desc" : "asc");
    setOrderBy(property);
  };

  const handleBeforePrint = React.useCallback(() => {
    tableRef.current?.setAttribute("style", "max-height: 500px");
  }, [tableRef]);

  const handleOnBeforeGetContent = React.useCallback(() => {
    setLoadingPrint(true);
    setLoadingPrintHeight(tableRef.current!.offsetHeight);

    return new Promise<void>((resolve) => {
      onBeforeGetContentResolve.current = resolve;

      setTimeout(() => {
        setLoadingPrint(false);
        resolve();
      }, 0);
    });
  }, [setLoadingPrint, tableRef]);

  const reactToPrintContent = React.useCallback(() => {
    tableRef.current?.setAttribute("style", "max-height: unset");
    return tableRef.current;
  }, [tableRef]);

  const handlePrint = useReactToPrint({
    content: reactToPrintContent,
    documentTitle: "Raspberry",
    onBeforeGetContent: handleOnBeforeGetContent,
    onBeforePrint: handleBeforePrint,
    removeAfterPrint: true,
  });

  columns.forEach((x) => {
    const toShow: IShow = { name: x.id, visible: true };
    displayedColumns.push(toShow);
  });

  const handleDisplayColumn = (e: React.MouseEvent<HTMLInputElement>) => {
    const name = e.currentTarget.innerText.toLowerCase();
    let modified = visibleColumns.find((x) => x.name.toLowerCase() === name);
    const origignal = visibleColumns.filter((x) => x.name !== name);
    modified!.visible = !modified!.visible;
    setVisibleColum([...origignal, modified!]);
  };

  return (
    <Fragment>
      {loadingPrint ? (
        <Segment style={{ maxHeight: 500, height: loadingPrintHeight }}>
          <Dimmer active inverted>
            <Loader size="small">Generando...</Loader>
          </Dimmer>
        </Segment>
      ) : (
        <div style={{ position: "relative" }}>
          <TableContainer
            component={Paper}
            style={{ maxHeight: 500 }}
            ref={tableRef}
          >
            {header && (
              <Table stickyHeader aria-label="sticky table">
                <TableHead key="pagination">
                  <TableRow>
                    {paginated && (
                      <TablePagination
                        rowsPerPageOptions={[
                          5,
                          10,
                          25,
                          50,
                          // 100,
                          // { label: "All", value: -1 },
                        ]}
                        style={{ width: actions === false ? "100%" : "50%" }}
                        count={data.length}
                        page={page}
                        rowsPerPage={rowsPerPage}
                        onChangePage={handleChangePage}
                        onChangeRowsPerPage={handleChangeRowsPerPage}
                        ActionsComponent={TablePaginationActions}
                      />
                    )}
                    {actions && (
                      <TableActions
                        paginated={paginated}
                        selected={selected ?? []}
                        showColumn={showColumn}
                        showFilter={showFilter}
                        filterComponent={filterComponent}
                        selectionActions={selectionActions}
                        filterAction={filterAction}
                        downloadAction={downloadAction}
                        handlePrint={handlePrint}
                        setShowFilter={setShowFilter}
                        setShowColumn={setShowColumn}
                      />
                    )}
                  </TableRow>
                </TableHead>
              </Table>
            )}
            <Table stickyHeader aria-label="sticky table">
              <TableHeader
                columns={columns}
                headers={headers}
                selectable={selectable}
                order={order}
                orderBy={orderBy}
                orderable={orderable}
                displayedColumns={visibleColumns}
                rowCount={data.length}
                numSelected={(selected ?? []).length}
                onRequestSort={handleRequestSort}
                onSelectAll={handleSelectAll}
              />
              <TableBody>
                {data.length === 0 ? (
                  <TableRow>
                    <TableCell
                      component="th"
                      colSpan={5}
                      align="center"
                      scope="role"
                    >
                      No data to show
                    </TableCell>
                  </TableRow>
                ) : (
                  (rowsPerPage > 0
                    ? stableSort(data, getComparator(order, orderBy)).slice(
                        page * rowsPerPage,
                        page * rowsPerPage + rowsPerPage
                      )
                    : stableSort(data, getComparator(order, orderBy))
                  ).map((datum) => {
                    const checked = selected
                      ? isSelected(Number(datum.id))
                      : false;
                    return (
                      <TableRow
                        hover
                        role="checkbox"
                        tabIndex={-1}
                        key={uuid()}
                        onClick={(event) => {
                          if (selected) handleSelect(Number(datum.id));
                        }}
                      >
                        {selectable && (
                          <TableCell padding="checkbox">
                            <Checkbox
                              checked={checked}
                              onChange={() => {
                                handleSelect(Number(datum.id));
                              }}
                            />
                          </TableCell>
                        )}
                        {columns.map((column) => {
                          return (
                            visibleColumns.find((v) => v.name === column.id)
                              ?.visible && (
                              <TableCell
                                key={uuid()}
                                align={column.align}
                                style={{
                                  backgroundColor: column.color
                                    ? column.color(datum)
                                    : "unset",
                                }}
                              >
                                {column.render === undefined
                                  ? datum[column.id]
                                  : column.render(datum)}
                              </TableCell>
                            )
                          );
                        })}
                      </TableRow>
                    );
                  })
                )}
              </TableBody>
            </Table>
          </TableContainer>
          {showFilter && filterComponent}
          {showColumn && (
            <TableColumns
              displayedColumns={displayedColumns}
              visibleColumns={visibleColumns}
              handleDisplayColumn={handleDisplayColumn}
            />
          )}
        </div>
      )}
    </Fragment>
  );
};

export default observer(TableComponent);
