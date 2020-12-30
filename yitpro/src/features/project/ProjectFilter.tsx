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

const ProjectFilter: React.FC = () => {
  const rootStore = useContext(RootStoreContext);
  const { filter, setFilter, clearFilter, filterProjects } = rootStore.projectStore;
  const { getClientOptions, clientOptions } = rootStore.optionStore;

  useEffect(() => {
    getClientOptions();
  }, [getClientOptions]);

  return (
    <Segment
      style={{ width: 450 }}
      className="filter-segment"
      inverted
      clearing
    >
      <Form
        onSubmit={() => {
          filterProjects();
        }}
      >
        <Grid>
          <Grid.Column width={10}>
            <label>Nombre</label>
            <input
              name="name"
              autoComplete="off"
              value={filter.name}
              type="text"
              placeholder="Proyecto..."
              onChange={setFilter}
            ></input>
          </Grid.Column>
          <Grid.Column width={6}>
            <label>Clave</label>
            <input
              name="code"
              autoComplete="off"
              value={filter.code}
              type="text"
              placeholder="Clave..."
              onChange={setFilter}
            ></input>
          </Grid.Column>
          <Grid.Column width={10}>
            <label>Cliente</label>
            <Dropdown
              search
              clearable
              selection
              pointing="top right"
              name="clientId"
              placeholder="Cliente..."
              value={filter.clientId}
              onChange={setFilter}
              options={clientOptions}
            />
          </Grid.Column>
          <Grid.Column width={6}>
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

export default observer(ProjectFilter);
