import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { observer } from "mobx-react-lite";
import React, { useContext } from "react";
import {
  ProSidebar,
  Menu,
  MenuItem,
  SubMenu,
  SidebarContent,
  SidebarHeader,
} from "react-pro-sidebar";
import { IMenu } from "../../app/models/menu";
import { Image } from "semantic-ui-react"
import { RootStoreContext } from "../../app/stores/rootStore";
import SidebarMenu from "./SidebarMenu";

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
  const rootStore = useContext(RootStoreContext);
  const { collapsed } = rootStore.commonStore;

  return (
    <ProSidebar collapsed={collapsed} className="proSidebar">
      <SidebarHeader>
        <Image src={"/assets/logo_sidebar.png"} size="large" centered />
      </SidebarHeader>
      <SidebarContent>
        <Menu>
          {menus.map((menu) => (menu.Menus ? (
            <SubMenu key={menu.Id}
              icon={menu.Level === 1 ? <FontAwesomeIcon icon="coffee" /> : null}
              prefix={menu.Level > 1 ? <FontAwesomeIcon icon="coffee" /> : null}
              title={menu.Description}
            >
              {menu.Menus.map((child) => (
                <SidebarMenu key={child.Id} menu={child} />
              ))}
            </SubMenu>
          ) : (
              <MenuItem key={menu.Id}
                icon={menu.Level === 1 ? <FontAwesomeIcon icon="coffee" /> : null}
                prefix={menu.Level > 1 ? <FontAwesomeIcon icon="coffee" /> : null}
              >
                {menu.Description}
              </MenuItem>
            )))}
        </Menu>
      </SidebarContent>
    </ProSidebar>
  );
};

export default observer(Sidebar);
