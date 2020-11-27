import { observer } from "mobx-react-lite";
import React, { Dispatch, SetStateAction, useContext } from "react";
import {
  Form,
  Segment,
  Grid,
  Select,
  Divider,
  Button,
} from "semantic-ui-react";
import { RootStoreContext } from "../../app/stores/rootStore";

interface IProps {
  setShowFilter: Dispatch<SetStateAction<boolean>>;
}

const RoleFilter: React.FC<IProps> = ({ setShowFilter }) => {
  const rootStore = useContext(RootStoreContext);
  const { filter, setFilter, clearFilter, filterRoles } = rootStore.roleStore;

  return (
    <Segment
      style={{ width: 300 }}
      className="filter-segment"
      inverted
      clearing
    >
      <Form
        onSubmit={() => {
          filterRoles();
          setShowFilter(false);
        }}
      >
        <Grid>
          <Grid.Column width={16}>
            <label>Rol</label>
            <input
              name="role"
              autoComplete="off"
              value={filter.role}
              type="text"
              placeholder="Rol..."
              onChange={setFilter}
            ></input>
          </Grid.Column>
          <Grid.Column width={8}>
            <label>Activo</label>
            <Select
              pointing="top right"
              name="active"
              value={filter.active}
              onChange={setFilter}
              options={[
                { key: "all", text: "Todos", value: "all" },
                { key: "yes", text: "Sí", value: "yes" },
                { key: "no", text: "No", value: "no" },
              ]}
            />
          </Grid.Column>
          <Grid.Column width={8}>
            <label>Protegido</label>
            <Select
              pointing="top right"
              name="protected"
              value={filter.protected}
              onChange={setFilter}
              options={[
                { key: "all", text: "Todos", value: "all" },
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

export default observer(RoleFilter);
