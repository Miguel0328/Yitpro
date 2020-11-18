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
import { Link } from "react-router-dom";
import { Icon, Image } from "semantic-ui-react";
import { RootStoreContext } from "../../app/stores/rootStore";
import SidebarMenu from "./SidebarMenu";

const Sidebar: React.FC = () => {
  const rootStore = useContext(RootStoreContext);
  const { collapsed } = rootStore.commonStore;
  const { user } = rootStore.profileStore;

  return (
    <ProSidebar collapsed={collapsed} className="prosidebar">
      <SidebarHeader>
        <Image src={"/assets/logo_sidebar.png"} size="large" centered />
      </SidebarHeader>
      <SidebarContent>
        <Menu>
          {user?.menus.map((menu) =>
            menu.submenus?.length > 0 ? (
              <SubMenu
                key={menu.menuId}
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
                key={menu.menuId}
                icon={menu.level === 1 ? <Icon className={menu.icon} /> : null}
                prefix={menu.level > 1 ? <Icon className={menu.icon} /> : null}
              >
                <Link to={menu.route ?? ""}>{menu.description}</Link>
              </MenuItem>
            )
          )}
        </Menu>
      </SidebarContent>
    </ProSidebar>
  );
};

export default observer(Sidebar);
