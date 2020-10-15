import {
  ProSidebar,
  Menu,
  MenuItem,
  SubMenu,
  SidebarContent,
  SidebarHeader,
} from "react-pro-sidebar";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { IMenu } from "../../app/models/menu";
import React from "react";

const SidebarMenu: React.FC<{ menu: IMenu }> = ({ menu }) =>
  menu.Menus ? (
    <SubMenu
      key={menu.Id}
      icon={menu.Level === 1 ? <FontAwesomeIcon icon="coffee" /> : null}
      prefix={menu.Level > 1 ? <FontAwesomeIcon icon="coffee" /> : null}
      title={menu.Description}
    >
      {menu.Menus.map((child) => (
        <SidebarMenu menu={child} />
      ))}
    </SubMenu>
  ) : (
    <MenuItem
      key={menu.Id}
      icon={menu.Level === 1 ? <FontAwesomeIcon icon="coffee" /> : null}
      prefix={menu.Level > 1 ? <FontAwesomeIcon icon="coffee" /> : null}
    >
      {menu.Description}
    </MenuItem>
  );

export default SidebarMenu;
