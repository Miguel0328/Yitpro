import React from "react";
import { Segment, Button, Header, Icon } from "semantic-ui-react";
import { Link } from "react-router-dom";

const Forbidden = () => {
  return (
    <Segment
      placeholder
      className="notfound"
    >
      <Header size="huge" icon>
        <Icon name="lock" size="massive" />
        No cuenta con permisos para acceder al recurso solicitado.
        <br />
        Contacte a su administrador de sistema.
      </Header>
      <Segment.Inline>
        <Button as={Link} to="/" primary>
          Regresar a Inicio
        </Button>
      </Segment.Inline>
    </Segment>
  );
};

export default Forbidden;
