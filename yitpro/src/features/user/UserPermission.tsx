import React, { SyntheticEvent, useContext, useEffect } from "react";
import { Button, Checkbox, Form, Grid, Icon, Segment } from "semantic-ui-react";
import TableComponent from "../../app/common/table/TableComponent";
import { observer } from "mobx-react-lite";
import { RootStoreContext } from "../../app/stores/root";
import { IColumn } from "../../app/models/table";
import { IUserPermission } from "../../app/models/user";

const UserPermission = () => {
  const rootStore = useContext(RootStoreContext);
  const {
    permissions,
    totalAccessChecked,
    totalCreateChecked,
    totalUpdateChecked,
    totalDeleteChecked,
    submitting,
    getPermissions,
    setAllChecked,
    clearPermissions,
    putPermissions,
  } = rootStore.userStore;

  useEffect(() => {
    getPermissions();

    return () => {
      clearPermissions();
    };
  }, [getPermissions, clearPermissions]);

  const headers: IColumn[] = [
    {
      id: "name",
      label: "Nombre",
      align: "left",
      width: "55%",
    },
    {
      id: "access",
      label: "Acceder",
      align: "center",
      width: "15%",
      render: () => (
        <Checkbox
          permissiontype="access"
          label="Acceder"
          indeterminate={totalAccessChecked === "indeterminate"}
          checked={totalAccessChecked === "all" ? true : false}
          onChange={(e: SyntheticEvent) => {
            setAllChecked(
              !e.currentTarget.classList.contains("checked"),
              e.currentTarget.getAttribute("permissiontype") as
                | "access"
                | "create"
                | "update"
            );
          }}
        />
      ),
    },
    {
      id: "create",
      label: "Crear",
      align: "center",
      width: "15%",
      render: () => (
        <Checkbox
          permissiontype="create"
          label="Crear"
          indeterminate={totalCreateChecked === "indeterminate"}
          checked={totalCreateChecked === "all" ? true : false}
          onChange={(e: SyntheticEvent) => {
            setAllChecked(
              !e.currentTarget.classList.contains("checked"),
              e.currentTarget.getAttribute("permissiontype") as
                | "access"
                | "create"
                | "update"
            );
          }}
        />
      ),
    },
    {
      id: "update",
      label: "Editar",
      align: "center",
      width: "15%",
      render: () => (
        <Checkbox
          permissiontype="update"
          label="Editar"
          indeterminate={totalUpdateChecked === "indeterminate"}
          checked={totalUpdateChecked === "all" ? true : false}
          onChange={(e: SyntheticEvent) => {
            setAllChecked(
              !e.currentTarget.classList.contains("checked"),
              e.currentTarget.getAttribute("permissiontype") as
                | "access"
                | "create"
                | "update"
            );
          }}
        />
      ),
    },
    {
      id: "delete",
      label: "Eliminar",
      align: "center",
      width: "15%",
      render: () => (
        <Checkbox
          permissiontype="delete"
          label="Eliminar"
          indeterminate={totalDeleteChecked === "indeterminate"}
          checked={totalDeleteChecked === "all" ? true : false}
          onChange={(e: SyntheticEvent) => {
            setAllChecked(
              !e.currentTarget.classList.contains("checked"),
              e.currentTarget.getAttribute("permissiontype") as
                | "access"
                | "create"
                | "update"
                | "delete"
            );
          }}
        />
      ),
    },
  ];

  const columns: IColumn[] = [
    {
      id: "name",
      label: "Nombre",
      align: "left",
      width: "55%",
      render: (permission: IUserPermission) => {
        return (
          <div
            style={{
              paddingLeft: permission.level === 1 ? 0 : permission.level * 10,
            }}
          >
            <Icon inverted circular color="grey" className={permission.icon} />{" "}
            {permission.name}
          </div>
        );
      },
    },
    {
      id: "access",
      label: "Acceder",
      align: "center",
      width: "15%",
      render: (permission: IUserPermission) => {
        return (
          <Checkbox
            toggle
            checked={permission.access}
            onChange={() => {
              permission.access = !permission.access;
            }}
          />
        );
      },
    },
    {
      id: "create",
      label: "Crear",
      align: "center",
      width: "15%",
      render: (permission: IUserPermission) => (
        <Checkbox
          toggle
          checked={permission.create}
          onChange={() => {
            permission.create = !permission.create;
          }}
        />
      ),
    },
    {
      id: "update",
      label: "Editar",
      align: "center",
      width: "15%",
      render: (permission: IUserPermission) => (
        <Checkbox
          toggle
          checked={permission.update}
          onChange={() => {
            permission.update = !permission.update;
          }}
        />
      ),
    },
    {
      id: "delete",
      label: "Eliminar",
      align: "center",
      width: "15%",
      render: (permission: IUserPermission) => (
        <Checkbox
          toggle
          checked={permission.delete}
          onChange={() => {
            permission.delete = !permission.delete;
          }}
        />
      ),
    },
  ];

  return (
    <Segment className="form-container" basic loading={submitting}>
      <Form onSubmit={() => putPermissions(permissions)} error>
        <Grid>
          <Grid.Column width={16}>
            <TableComponent
              key="permission"
              columns={columns}
              data={permissions}
              headers={headers}
              header={false}
              orderable={false}
            />
          </Grid.Column>
          <Grid.Column width={16} textAlign="right">
            <Button
              disabled={submitting}
              type="submit"
              color="vk"
              content="Guardar"
            />
          </Grid.Column>
        </Grid>
      </Form>
    </Segment>
  );
};

export default observer(UserPermission);
