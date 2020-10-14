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
// import { Label, Menu, MenuItem } from "semantic-ui-react";

const Sidebar = () => {
  return (
    <ProSidebar className="proSidebar">
      <SidebarHeader className="header-height">
        <Image src={"/assets/logo_sidebar.png"} size="large" centered />
      </SidebarHeader>
      <SidebarContent>
        <Menu>
          <MenuItem icon={<FontAwesomeIcon icon="coffee" />}>
            Dashboard
          </MenuItem>
          <SubMenu title="Components">
            <MenuItem>Component 1</MenuItem>
            <MenuItem>Component 2</MenuItem>
          </SubMenu>
        </Menu>
      </SidebarContent>
    </ProSidebar>
  );
};

export default Sidebar;
