import React from "react";
import { Segment, Button, Header, Icon } from "semantic-ui-react";
import { Link } from "react-router-dom";

const NotFound = () => {
  return (
    <Segment
      placeholder
      className="notfound"
    >
      <Header size="huge" icon>
        <Icon name="search" size="massive" />
        No encontramos el recurso solicitado.
      </Header>
      <Segment.Inline>
        <Button as={Link} to="/" primary>
          Regresar a Inicio
        </Button>
      </Segment.Inline>
    </Segment>
  );
};

export default NotFound;
