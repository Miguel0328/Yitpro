import { Switch } from "@material-ui/core";
import { observer } from "mobx-react-lite";
import React, { useContext, useEffect } from "react";
import { Button, Segment } from "semantic-ui-react";
import TableComponent from "../../app/common/table/TableComponent";
import { IClasification } from "../../app/models/phase";
import { IColumn } from "../../app/models/table";
import { RootStoreContext } from "../../app/stores/root";

const ClasificationTable = () => {
  const rootStore = useContext(RootStoreContext);
  const {
    loadingClasifications,
    filteredClasifications: clasifications,
    clasifications: allClasifications,
    phase: current,
    clearClasifications,
    getById,
    put,
    putAll,
    filterClasifications,
  } = rootStore.phaseStore;
  const { openConfirmationModal } = rootStore.modalStore;

  useEffect(() => {
    return () => {
      clearClasifications();
    };
  }, [clearClasifications]);

  const headers: IColumn[] = [
    {
      id: "name",
      label: "Nombre",
      align: "left",
      width: "80%",
    },
    {
      id: "active",
      label: "Agregar",
      align: "center",
      width: "20%",
      orderable: false,
      render: () =>
        !current ? null : (
          <Button
            onClick={() => {
              openConfirmationModal(
                allClasifications.length.toString(),
                "confirmation",
                () => {
                  putAll(current!.id).then(() => {
                    getById(current!.id);
                  });
                },
                true
              );
            }}
            as="a"
            style={{ color: "white !important" }}
            className="a-button white"
          >
            Agregar todos
          </Button>
        ),
    },
  ];

  const columns: IColumn[] = [
    {
      id: "name",
      label: "Nombre",
      align: "left",
      width: "80%",
    },
    {
      id: "active",
      label: "Agregar",
      align: "center",
      width: "20%",
      render: (clasification: IClasification) => (
        <div className="table-actions">
          <Switch
            size="small"
            color="primary"
            checked={clasification.active}
            onChange={() => {
              clasification.active = !clasification.active;
              put(clasification)
                .catch(() => (clasification.active = !clasification.active))
                .then(() => {
                  getById(clasification.phaseId);
                });
            }}
          />
        </div>
      ),
    },
  ];

  return (
    <Segment loading={loadingClasifications} className="segment-table">
      <TableComponent
        orderColumn="active"
        orderDirection="desc"
        title="Clasificaciones"
        columns={columns}
        headers={headers}
        data={clasifications}
        filterAction={filterClasifications}
        paginated={false}
        hideable={false}
        printable={false}
      />
    </Segment>
  );
};

export default observer(ClasificationTable);
