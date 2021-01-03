import { observer } from "mobx-react-lite";
import React, { useContext } from "react";
import { Button, Header, Segment } from "semantic-ui-react";
import { RootStoreContext } from "../../../../app/stores/root";
import ProjectTeamRemaining from "./ProjectTeamRemaining";

const ProjectTeamHeader = () => {
  const rootStore = useContext(RootStoreContext);
  const { openModal } = rootStore.modalStore;

  return (
    <Segment clearing className="segment-header">
      <Header
        as="h2"
        style={{ margin: 0 }}
        icon="users"
        className="icon-header"
        content="Equipo"
        floated="left"
      />
      <Button
        primary
        content="Agregar"
        floated="right"
        icon="add user"
        onClick={() => {
          openModal(<ProjectTeamRemaining />, "small", "Agregar usuarios");
        }}
      />
    </Segment>
  );
};

export default observer(ProjectTeamHeader);
