import React, { useContext } from "react";
import { Menu, Container, Dropdown, Image } from "semantic-ui-react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { RootStoreContext } from "../../app/stores/rootStore";
import { observer } from "mobx-react-lite";

const NavBar: React.FC = () => {
  const rootStore = useContext(RootStoreContext);
  const { collapsed, setCollapsed } = rootStore.commonStore;

  return (
    <Menu className="navbar header-height" fixed="top" inverted>
      <Container className="navbar-container">
        <Menu.Item className="menu-collapse-item">
          {collapsed ?
            <FontAwesomeIcon className="menu" size="lg" icon="long-arrow-alt-right" onClick={setCollapsed} />
            :
            <FontAwesomeIcon className="menu" size="lg" icon="long-arrow-alt-left" onClick={setCollapsed} />}
        </Menu.Item>
        <Menu.Item header>
          <img src="/assets/logo.png" alt="Logo" style={{ marginRight: 10 }} />
          Organización: Raspberry Pi 4.0
        </Menu.Item>
        <Menu.Menu position="right">
          <Menu.Item>
            <FontAwesomeIcon className="menu" icon="key" />
          </Menu.Item>{" "}
          <Menu.Item>
            <FontAwesomeIcon className="menu" icon="bell" />
          </Menu.Item>
          <Menu.Item>
            <Image avatar spaced="right" src={"/assets/avatar.jpg"} />
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

export default observer(NavBar);
