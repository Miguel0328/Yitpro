import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import React, { useState } from "react";
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
import { Image } from "semantic-ui-react";
import { IMenu } from "../../app/models/menu";
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

const Sidebar = () => {
  return (
    <ProSidebar className="proSidebar">
      <SidebarHeader>
        <Image src={"/assets/logo_sidebar.png"} size="large" centered />
      </SidebarHeader>
      <SidebarContent>
        <Menu>
          {menus.map((menu) =>(<SidebarMenu key={menu.Id} menu={menu}/>))}
        </Menu>
      </SidebarContent>
    </ProSidebar>
  );
};

export default Sidebar;
