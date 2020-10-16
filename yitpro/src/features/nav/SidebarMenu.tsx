import {
  MenuItem,
  SubMenu,
} from "react-pro-sidebar";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { IMenu } from "../../app/models/menu";
import React from "react";
import { observer } from "mobx-react-lite";

const SidebarMenu: React.FC<{ menu: IMenu }> = ({ menu }) =>
  menu.Menus ? (
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
    );

export default observer(SidebarMenu);
