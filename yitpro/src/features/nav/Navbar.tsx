import React, { useContext } from "react";
import { Menu, Container, Dropdown, Image, Icon } from "semantic-ui-react";
import { RootStoreContext } from "../../app/stores/root";
import { observer } from "mobx-react-lite";
import ActivityForm from "../activity/activityForm/ActivityFormDetail";
import ActivityTab from "../activity/activityForm/ActivityFormTab";

const NavBar: React.FC = () => {
  const rootStore = useContext(RootStoreContext);
  const { collapsed, setCollapsed } = rootStore.commonStore;
  const { logout, user } = rootStore.profileStore;
  const { openModal } = rootStore.modalStore;
  const { setActivityId } = rootStore.activityStore;

  return (
    <Menu className="navbar header-height" fixed="top" inverted>
      <Container className="navbar-container">
        <Menu.Item className="menu-collapse-item">
          {collapsed ? (
            <Icon
              className="icon-menu"
              name="long arrow alternate right"
              onClick={setCollapsed}
            />
          ) : (
            <Icon
              className="icon-menu"
              name="long arrow alternate left"
              onClick={setCollapsed}
            />
          )}
        </Menu.Item>
        <Menu.Item header>
          <img src="/assets/logo.png" alt="Logo" style={{ marginRight: 10 }} />
          Organización: Raspberry Pi 4.0
        </Menu.Item>
        <Menu.Menu position="right">
          <Menu.Item onClick={() => {
            setActivityId(0);
            openModal(<ActivityForm />, "medium", "Nueva Actividad")
          }}>
            <Icon className="icon-menu" name="pencil" />
          </Menu.Item>{" "}
          <Menu.Item>
            <Icon className="icon-menu" name="key" />
          </Menu.Item>{" "}
          <Menu.Item>
            <Icon className="icon-menu" name="bell" />
          </Menu.Item>
          <Menu.Item>
            <Image
              avatar
              spaced="right"
              src={user?.photo ?? "/assets/avatar.png"}
            />
            <Dropdown
              className="nav-dropdown"
              pointing="top right"
              text={user?.name}
            >
              <Dropdown.Menu>
                <Dropdown.Item text="My profile" icon="user" />
                <Dropdown.Item text="Logout" icon="power" onClick={logout} />
              </Dropdown.Menu>
            </Dropdown>
          </Menu.Item>
        </Menu.Menu>
      </Container>
    </Menu>
  );
};

export default observer(NavBar);
