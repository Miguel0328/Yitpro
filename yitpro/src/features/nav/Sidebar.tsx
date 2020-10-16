import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { observer } from "mobx-react-lite";
import React, { Fragment, useContext, useState } from "react";
// import {
//   ProSidebar,
//   SidebarHeader,
//   MenuItem,
//   SubMenu,
//   SidebarContent,
//   SubMenu,
// } from "react-pro-sidebar";
import {
  ProSidebar,
  Menu,
  MenuItem,
  SubMenu,
  SidebarContent,
  SidebarHeader,
} from "react-pro-sidebar";
import { Button, ButtonContent, Container, Image, Label, SidebarPusher } from "semantic-ui-react";
import { IMenu } from "../../app/models/menu";
import { RootStoreContext } from "../../app/stores/rootStore";
import SidebarMenu from "./SidebarMenu";
// import { Label, Menu, MenuItem } from "semantic-ui-react";

const menus: IMenu[] = [
  {
    Id: 1,
    Description: "Dashboard",
    Controller: "",
    Action: "",
    Order: 1,
    Level: 1,
    Icon: "",
  },
  {
    Id: 2,
    Description: "Kanban",
    Controller: "",
    Action: "",
    Order: 1,
    Level: 1,
    Icon: "",
    Menus: [
      {
        Id: 4,
        IdParent: 2,
        Description: "Reportes 1",
        Controller: "",
        Action: "",
        Order: 1,
        Level: 2,
        Icon: "",
      },
    ],
  },
  {
    Id: 3,
    Description: "Panel de actividades",
    Controller: "",
    Action: "",
    Order: 1,
    Level: 1,
    Icon: "",
    Menus: [
      {
        Id: 5,
        IdParent: 3,
        Description: "Reportes 2",
        Controller: "",
        Action: "",
        Order: 1,
        Level: 2,
        Icon: "",
        Menus: [
          {
            Id: 5,
            IdParent: 3,
            Description: "Reportes 2.1",
            Controller: "",
            Action: "",
            Order: 1,
            Level: 3,
            Icon: "",

          },
          {
            Id: 6,
            IdParent: 3,
            Description: "Reportes 2.2",
            Controller: "",
            Action: "",
            Order: 1,
            Level: 3,
            Icon: "",
            Menus: [
              {
                Id: 5,
                IdParent: 3,
                Description: "Reportes 2.1.1",
                Controller: "",
                Action: "",
                Order: 1,
                Level: 3,
                Icon: "",

              },
              {
                Id: 6,
                IdParent: 3,
                Description: "Reportes 2.1.2",
                Controller: "",
                Action: "",
                Order: 1,
                Level: 3,
                Icon: "",
              },]
          },
        ]
      },
      {
        Id: 6,
        IdParent: 3,
        Description: "Reportes 3",
        Controller: "",
        Action: "",
        Order: 1,
        Level: 2,
        Icon: "",
      },
    ],
  },
];

const Sidebar: React.FC = () => {
  // const [collapsed, setCollapsed] = useState(true);
  const rootStore = useContext(RootStoreContext);
  const { collapsed } = rootStore.commonStore;

  return (
    <ProSidebar collapsed={collapsed} className="proSidebar">
      {/* <Container textAlign="center">
        {collapsed ?
          <FontAwesomeIcon size="lg" icon="long-arrow-alt-right" onClick={() => setCollapsed(!collapsed)} />
          :
          <FontAwesomeIcon size="lg" icon="long-arrow-alt-left" onClick={() => setCollapsed(!collapsed)} />
        }
      </Container> */}
      <SidebarHeader>
        <Image src={"/assets/logo_sidebar.png"} size="large" centered />
      </SidebarHeader>
      <SidebarContent>
        <Menu>
          {menus.map((menu) => (menu.Menus ? (
            <SubMenu
              icon={menu.Level === 1 ? <FontAwesomeIcon icon="coffee" /> : null}
              prefix={menu.Level > 1 ? <FontAwesomeIcon icon="coffee" /> : null}
              title={menu.Description}
            >
              {menu.Menus.map((child) => (
                <SidebarMenu key={child.Id} menu={child} />
              ))}
            </SubMenu>
          ) : (
              <MenuItem
                icon={menu.Level === 1 ? <FontAwesomeIcon icon="coffee" /> : null}
                prefix={menu.Level > 1 ? <FontAwesomeIcon icon="coffee" /> : null}
              >
                {menu.Description}
              </MenuItem>
            )))}
        </Menu>

        {/* <Menu iconShape="square">
          <MenuItem icon={<FontAwesomeIcon icon="coffee" />}>Dashboard</MenuItem>
          <SubMenu title="Components" icon={<FontAwesomeIcon icon="coffee" />}>
            <MenuItem>Component 1</MenuItem>
            <MenuItem>Component 2</MenuItem>
            <SubMenu title="Components" icon={<FontAwesomeIcon icon="coffee" />}>
              <MenuItem>Component 1</MenuItem>
              <MenuItem>Component 2</MenuItem>
            </SubMenu>
          </SubMenu>
        </Menu> */}
      </SidebarContent>
    </ProSidebar>
  );
};

export default observer(Sidebar);
