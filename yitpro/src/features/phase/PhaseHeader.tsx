import React from "react";
import { Header, Segment } from "semantic-ui-react";

const PhaseHeader = () => {
  return (
    <Segment clearing className="segment-header">
      <Header
        as="h2"
        style={{ margin: 0 }}
        icon="angle double up"
        className="icon-header"
        content="Fases"
        floated="left"
      />
    </Segment>
  );
};

export default PhaseHeader;
