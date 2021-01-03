import { Switch } from "@material-ui/core";
import { observer } from "mobx-react-lite";
import React, { useContext, useEffect } from "react";
import { Segment, Icon, Image } from "semantic-ui-react";
import TableComponent from "../../app/common/table/TableComponent";
import { IColumn } from "../../app/models/table";
import { IUser } from "../../app/models/user";
import { RootStoreContext } from "../../app/stores/root";
import UserFilter from "./UserFilter";
import UserForm from "./UserForm";
import UserPermission from "./UserPermission";

const UserTable = () => {
  const rootStore = useContext(RootStoreContext);
  const {
    loading,
    filtered: users,
    get,
    clearUsers,
    setUserId,
    putEnabled,
    download,
    filterByText,
  } = rootStore.userStore;
  const { openModal } = rootStore.modalStore;

  useEffect(() => {
    get();

    return () => {
      clearUsers();
    };
  }, [get, clearUsers]);

  const columns: IColumn[] = [
    {
      id: "name",
      label: "Nombre",
      align: "left",
      width: "24%",
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
      width: "20%",
    },
    {
      id: "role",
      label: "Rol",
      align: "left",
      width: "16%",
    },
    {
      id: "department",
      label: "Departamento",
      align: "left",
      width: "16%",
    },
    {
      id: "password",
      label: "Contraseña",
      align: "center",
      width: "8%",
      orderable: false,
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
      orderable: false,
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
      orderable: false,
      render: (user: IUser) => (
        <div className="table-actions">
          <Icon
            name="edit"
            className="icon-table"
            onClick={() => {
              console.log("Editando");
              setUserId(user.id);
              openModal(<UserForm />, "medium", "Editar: " + user.name);
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
      <TableComponent
        orderColumn="name"
        columns={columns}
        data={users}
        filterAction={filterByText}
        downloadAction={download}
        filterComponent={<UserFilter />}
      />
    </Segment>
  );
};

export default observer(UserTable);
