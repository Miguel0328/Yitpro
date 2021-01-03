import { observer } from "mobx-react-lite";
import React, { useContext } from "react";
import {
  Form,
  Segment,
  Grid,
  Divider,
  Button,
  Dropdown,
} from "semantic-ui-react";
import { RootStoreContext } from "../../app/stores/root";

const CatalogFilter: React.FC = () => {
  const rootStore = useContext(RootStoreContext);
  const {
    filter,
    setFilter,
    clearFilter,
    filterCatalogs,
  } = rootStore.catalogStore;

  return (
    <Segment
      style={{ width: 300 }}
      className="filter-segment"
      inverted
      clearing
    >
      <Form
        onSubmit={() => {
          filterCatalogs();
        }}
      >
        <Grid>
          <Grid.Column width={16}>
            <label>Descripción</label>
            <input
              name="description"
              autoComplete="off"
              value={filter.description}
              type="text"
              placeholder="Descripción..."
              onChange={setFilter}
            ></input>
          </Grid.Column>
          <Grid.Column width={8}>
            <label>Alias</label>
            <input
              name="alias"
              autoComplete="off"
              value={filter.alias}
              type="text"
              placeholder="Alias..."
              onChange={setFilter}
            ></input>
          </Grid.Column>
          <Grid.Column width={8}>
            <label>Activo</label>
            <Dropdown
              selection
              clearable
              placeholder="Todos..."
              pointing="top right"
              name="active"
              value={filter.active}
              onChange={setFilter}
              options={[
                { key: "yes", text: "Sí", value: "yes" },
                { key: "no", text: "No", value: "no" },
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

export default observer(CatalogFilter);
