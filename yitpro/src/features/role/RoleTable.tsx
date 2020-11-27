import { Switch } from "@material-ui/core";
import { observer } from "mobx-react-lite";
import React, { useContext } from "react";
import { Segment, Icon } from "semantic-ui-react";
import TableComponent from "../../app/common/table/TableComponent";
import { IRole } from "../../app/models/role";
import { IColumn } from "../../app/models/table";
import { RootStoreContext } from "../../app/stores/rootStore";
import RoleForm from "./RoleForm";
import RolePermission from "./RolePermission";

const RoleTable = () => {
  const rootStore = useContext(RootStoreContext);
  const {
    loading,
    filtered: roles,
    setRole,
    putEnabled,
  } = rootStore.roleStore;
  const { openModal } = rootStore.modalStore;

  const columns: IColumn[] = [
    {
      id: "name",
      label: "Nombre",
      align: "left",
      width: "76%",
    },
    {
      id: "protected",
      label: "Protegido",
      align: "center",
      width: "8%",
      render: (role: IRole) => (role.protected ? "Sí" : "No"),
    },
    {
      id: "permission",
      label: "Permisos",
      align: "center",
      width: "8%",
      render: (role: IRole) => (
        <Icon
          name="lock"
          className="icon-table"
          onClick={() => {
            setRole(role.id);
            openModal(<RolePermission />, "small", "Permisos: " + role.name);
          }}
        />
      ),
    },
    {
      id: "actions",
      label: "Acciones",
      align: "center",
      width: "8%",
      render: (role: IRole) =>
        !role.protected && (
          <div className="table-actions">
            <Icon
              name="edit"
              className="icon-table"
              onClick={() => {
                setRole(role.id);
                openModal(<RoleForm />, "tiny", "Editar: " + role.name);
              }}
            />
            <Switch
              size="small"
              color="primary"
              checked={role.active}
              onChange={() => {
                role.active = !role.active;
                putEnabled(role).catch(() => (role.active = !role.active));
              }}
            />
          </div>
        ),
    },
  ];

  return (
    <Segment loading={loading} className="segment-table">
      <TableComponent columns={columns} data={roles} />
    </Segment>
  );
};

export default observer(RoleTable);
