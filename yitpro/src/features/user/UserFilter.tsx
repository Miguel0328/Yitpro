import { observer } from "mobx-react-lite";
import React, { useContext, useEffect } from "react";
import {
  Form,
  Segment,
  Grid,
  Divider,
  Button,
  Dropdown,
} from "semantic-ui-react";
import { RootStoreContext } from "../../app/stores/root";

const UserFilter: React.FC = () => {
  const rootStore = useContext(RootStoreContext);
  const { filter, setFilter, clearFilter, filterUsers } = rootStore.userStore;
  const { getRoleOptions, roleOptions } = rootStore.optionStore;

  useEffect(() => {
    getRoleOptions();
  }, [getRoleOptions]);

  return (
    <Segment
      style={{ width: 450 }}
      className="filter-segment"
      inverted
      clearing
    >
      <Form
        onSubmit={() => {
          filterUsers();
        }}
      >
        <Grid>
          <Grid.Column width={8}>
            <label>Usuario</label>
            <input
              name="name"
              autoComplete="off"
              value={filter.name}
              type="text"
              placeholder="Usuario..."
              onChange={setFilter}
            ></input>
          </Grid.Column>
          <Grid.Column width={8}>
            <label>Correo</label>
            <input
              name="email"
              autoComplete="off"
              value={filter.email}
              type="text"
              placeholder="Correo..."
              onChange={setFilter}
            ></input>
          </Grid.Column>
          <Grid.Column width={8}>
            <label>Rol</label>
            <Dropdown
              search
              clearable
              selection
              pointing="top right"
              name="roleId"
              placeholder="Rol..."
              value={filter.roleId}
              onChange={setFilter}
              options={roleOptions}
            />
          </Grid.Column>
          <Grid.Column width={8}>
            <label>Activo</label>
            <Dropdown
              clearable
              selection
              pointing="top right"
              name="active"
              value={filter.active}
              onChange={setFilter}
              placeholder="Todos..."
              options={[
                { key: "yes", text: "Sí", value: true },
                { key: "no", text: "No", value: false },
              ]}
            />
          </Grid.Column>
        </Grid>
        <Divider inverted />
        <Button
          content="Limpiar"
          floated="left"
          compact
          onClick={clearFilter}
          type="button"
        />
        <Button
          content="Filtrar"
          floated="right"
          compact
          color="blue"
          type="submit"
        />
      </Form>
    </Segment>
  );
};

export default observer(UserFilter);
