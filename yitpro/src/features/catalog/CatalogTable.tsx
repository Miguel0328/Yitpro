import { Switch } from "@material-ui/core";
import { observer } from "mobx-react-lite";
import React, { useContext, useEffect } from "react";
import { Segment, Icon } from "semantic-ui-react";
import TableComponent from "../../app/common/table/TableComponent";
import { ICatalog } from "../../app/models/catalog";
import { IColumn } from "../../app/models/table";
import { RootStoreContext } from "../../app/stores/root";
import CatalogFilter from "./CatalogFilter";
import CatalogForm from "./CatalogForm";

const CatalogTable = () => {
  const rootStore = useContext(RootStoreContext);
  const {
    loading,
    filtered: catalogs,
    clearCatalogs,
    setCatalog,
    putEnabled,
    download,
    filterCatalogs,
  } = rootStore.catalogStore;
  const { openModal } = rootStore.modalStore;

  useEffect(() => {
    return () => {
      clearCatalogs();
    };
  }, [clearCatalogs]);

  const columns: IColumn[] = [
    {
      id: "alias",
      label: "Alias",
      align: "left",
      width: "16%",
    },
    {
      id: "description",
      label: "Descripción",
      align: "left",
      width: "68%",
    },
    {
      id: "protected",
      label: "Protegido",
      align: "center",
      width: "8%",
      render: (catalog: ICatalog) => (catalog.protected ? "Sí" : "No"),
    },
    {
      id: "actions",
      label: "Acciones",
      align: "center",
      width: "8%",
      orderable: false,
      render: (catalog: ICatalog) =>
        !catalog.protected && (
          <div className="table-actions">
            <Icon
              name="edit"
              className="icon-table"
              onClick={() => {
                setCatalog(catalog.id);
                openModal(<CatalogForm />, "tiny", "Editar: " + catalog.alias);
              }}
            />
            <Switch
              size="small"
              color="primary"
              checked={catalog.active}
              onChange={() => {
                catalog.active = !catalog.active;
                putEnabled(catalog).catch(
                  () => (catalog.active = !catalog.active)
                );
              }}
            />
          </div>
        ),
    },
  ];

  return (
    <Segment loading={loading} className="segment-table">
      <TableComponent
        filterComponent={<CatalogFilter />}
        orderColumn="alias"
        columns={columns}
        data={catalogs}
        filterAction={filterCatalogs}
        downloadAction={download}
      />
    </Segment>
  );
};

export default observer(CatalogTable);
