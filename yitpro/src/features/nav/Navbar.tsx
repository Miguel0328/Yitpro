import React from "react";
import { Menu, Container, Button, Dropdown, Image } from "semantic-ui-react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

const NavBar: React.FC = () => {
  return (
    <Menu className="navbar header-height" fixed="top" inverted>
      <Container className="navbar-container">
        <Menu.Item header>
          <img src="/assets/logo.png" alt="Logo" style={{ marginRight: 10 }} />
          Organización: Raspberry Pi 4.0
        </Menu.Item>
        <Menu.Menu position="right">
          <Menu.Item>
            <FontAwesomeIcon icon="key" />
          </Menu.Item>{" "}
          <Menu.Item>
            <FontAwesomeIcon icon="bell" />
          </Menu.Item>
          <Menu.Item>
            <Image avatar spaced="right" src={"/assets/logo.png"} />
            <Dropdown pointing="top right" text="Martin Hernandez">
              <Dropdown.Menu>
                <Dropdown.Item text="My profile" icon="user" />
                <Dropdown.Item text="Logout" icon="power" />
              </Dropdown.Menu>
            </Dropdown>
          </Menu.Item>
        </Menu.Menu>
      </Container>
    </Menu>
  );
};

export default NavBar;
