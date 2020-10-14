import React from "react";
import { Menu, Container, Button, Dropdown, Image } from "semantic-ui-react";

const NavBar: React.FC = () => {
  return (
    <Menu className="navbar header-height" fixed="top" inverted>
      <Container className="navbar-container">
        <Menu.Item header>
          <img src="/assets/logo.png" alt="Logo" style={{ marginRight: 10 }} />
          Reactivities
        </Menu.Item>
        <Menu.Item name="Activities" />
        <Menu.Item>
          <Button
            positive
            content="Create Activity"
          />
        </Menu.Item>
        <Menu.Item position="right">
          <Image avatar spaced="right" src={"/assets/logo.png"} />
          <Dropdown pointing="top right" text="Martin">
            <Dropdown.Menu>
              <Dropdown.Item text="My profile" icon="user" />
              <Dropdown.Item text="Logout" icon="power" />
            </Dropdown.Menu>
          </Dropdown>
        </Menu.Item>
      </Container>
    </Menu>
  );
};

export default NavBar;
