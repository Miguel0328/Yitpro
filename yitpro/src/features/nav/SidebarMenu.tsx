import { MenuItem, SubMenu } from "react-pro-sidebar";
import React from "react";
import { observer } from "mobx-react-lite";
import { Icon } from "semantic-ui-react";
import { IMenu } from "../../app/models/profile";
import { Link } from "react-router-dom";

const SidebarMenu: React.FC<{ menu: IMenu }> = ({ menu }) =>
  menu.submenus?.length > 0 ? (
    <SubMenu
      icon={menu.level === 1 ? <Icon className={menu.icon} /> : null}
      prefix={menu.level > 1 ? <Icon className={menu.icon} /> : null}
      title={menu.description}
    >
      {menu.submenus.map((child) => (
        <SidebarMenu key={child.menuId} menu={child} />
      ))}
    </SubMenu>
  ) : (
    <MenuItem
      icon={menu.level === 1 ? <Icon className={menu.icon} /> : null}
      prefix={menu.level > 1 ? <Icon className={menu.icon} /> : null}
    >
      <Link to={menu.route ?? ""}>{menu.description}</Link>
    </MenuItem>
  );

export default observer(SidebarMenu);
