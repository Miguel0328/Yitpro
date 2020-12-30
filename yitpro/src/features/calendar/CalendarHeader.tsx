import React from "react";
import { Segment, Header } from "semantic-ui-react";

const CalendarHeader = () => {
  return (
    <Segment clearing className="segment-header">
      <Header
        as="h2"
        style={{ margin: 0 }}
        icon="calendar alternate outline"
        className="icon-header"
        content="Calendario"
        floated="left"
      />
    </Segment>
  );
};

export default CalendarHeader;
