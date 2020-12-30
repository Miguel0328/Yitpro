import { Switch } from "@material-ui/core";
import { observer } from "mobx-react-lite";
import React, { useContext, useEffect } from "react";
import { Segment, Icon } from "semantic-ui-react";
import TableComponent from "../../app/common/table/TableComponent";
import { IClient } from "../../app/models/client";
import { IColumn } from "../../app/models/table";
import { RootStoreContext } from "../../app/stores/root";
import ClientFilter from "./ClientFilter";
import ClientForm from "./ClientForm";

const ClientTable = () => {
  const rootStore = useContext(RootStoreContext);
  const {
    loading,
    filtered: clients,
    get,
    clearClients,
    setClient,
    putEnabled,
    download,
    filterClients,
  } = rootStore.clientStore;
  const { openModal } = rootStore.modalStore;

  useEffect(() => {
    get();

    return () => {
      clearClients();
    };
  }, [get, clearClients]);

  const columns: IColumn[] = [
    {
      id: "name",
      label: "Nombre",
      align: "left",
      width: "78%",
    },
    {
      id: "projectCount",
      label: "No. Proyectos",
      align: "right",
      width: "14%",
    },
    {
      id: "actions",
      label: "Acciones",
      align: "center",
      width: "8%",
      orderable: false,
      render: (client: IClient) => (
        <div className="table-actions">
          <Icon
            name="edit"
            className="icon-table"
            onClick={() => {
              setClient(client.id);
              openModal(<ClientForm />, "tiny", "Editar: " + client.name);
            }}
          />
          <Switch
            size="small"
            color="primary"
            checked={client.active}
            onChange={() => {
              client.active = !client.active;
              putEnabled(client).catch(() => (client.active = !client.active));
            }}
          />
        </div>
      ),
    },
  ];

  return (
    <Segment loading={loading} className="segment-table">
      <TableComponent
        filterComponent={<ClientFilter />}
        orderColumn="name"
        columns={columns}
        data={clients}
        filterAction={filterClients}
        downloadAction={download}
      />
    </Segment>
  );
};

export default observer(ClientTable);
