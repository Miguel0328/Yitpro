import React from "react";
import { Button, Header, Segment } from "semantic-ui-react";

const ProjectTeamHeader = () => {
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
      <Button primary content="Agregar" floated="right" icon="add user" />
    </Segment>
  );
};

export default ProjectTeamHeader;
