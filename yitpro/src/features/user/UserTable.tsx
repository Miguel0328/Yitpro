import { Switch } from "@material-ui/core";
import { observer } from "mobx-react-lite";
import React, { useContext } from "react";
import { Segment, Icon, Image } from "semantic-ui-react";
import TableComponent from "../../app/common/table/TableComponent";
import { IColumn } from "../../app/models/table";
import { IUser } from "../../app/models/user";
import { RootStoreContext } from "../../app/stores/rootStore";
import UserForm from "./UserForm";
import UserPermission from "./UserPermission";

const RoleTable = () => {
  const rootStore = useContext(RootStoreContext);
  const {
    loading,
    filtered: users,
    setUserId,
    putEnabled,
  } = rootStore.userStore;
  const { openModal } = rootStore.modalStore;

  const columns: IColumn[] = [
    {
      id: "name",
      label: "Nombre",
      align: "left",
      width: "26%",
      // color: (user: IUser) => (user.active ? undefined : "#ee9f9f"),
      render: (user: IUser) => (
        <div
          style={{
            display: "flex",
            flexDirection: "row",
            justifyContent: "flex-start",
            alignItems: "center",
          }}
        >
          <Image src={user.photo ? user.photo : "/assets/avatar.png"} avatar />
          <span>{user.name}</span>
        </div>
      ),
    },
    {
      id: "email",
      label: "Correo",
      align: "left",
      width: "25%",
    },
    {
      id: "role",
      label: "Rol",
      align: "left",
      width: "25%",
    },
    {
      id: "password",
      label: "Contraseña",
      align: "center",
      width: "8%",
      render: (user: IUser) => (
        <Icon
          name="user secret"
          className="icon-table"
          onClick={() => {
            alert("Hola");
            //   setRole(user.id);
            //   openModal(<RolePermission />, "small", "Permisos: " + user.name);
          }}
        />
      ),
    },
    {
      id: "permission",
      label: "Permisos",
      align: "center",
      width: "8%",
      render: (user: IUser) => (
        <Icon
          name="lock"
          className="icon-table"
          onClick={() => {
            setUserId(user.id);
            openModal(<UserPermission />, "small", "Permisos: " + user.name);
          }}
        />
      ),
    },
    {
      id: "actions",
      label: "Acciones",
      align: "center",
      width: "8%",
      render: (user: IUser) => (
        <div className="table-actions">
          <Icon
            name="edit"
            className="icon-table"
            onClick={() => {
              setUserId(user.id);
              openModal(<UserForm />, "small", "Editar: " + user.name);
            }}
          />
          <Switch
            size="small"
            color="primary"
            checked={user.active}
            onChange={() => {
              user.active = !user.active;
              putEnabled(user).catch(() => (user.active = !user.active));
            }}
          />
        </div>
      ),
    },
  ];

  return (
    <Segment loading={loading} className="segment-table">
      <TableComponent columns={columns} data={users} />
    </Segment>
  );
};

export default observer(RoleTable);
