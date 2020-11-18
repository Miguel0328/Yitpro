import { observer } from "mobx-react-lite";
import React, { useContext, useEffect } from "react";
import { Divider, Segment } from "semantic-ui-react";
import { RootStoreContext } from "../../app/stores/rootStore";
import RoleHeader from "./RoleHeader";
import RoleTable from "./RoleTable";

const Role = () => {
  const rootStore = useContext(RootStoreContext);
  const { index, getRoles, clearRoles } = rootStore.roleStore;
  const { loadingIndex } = rootStore.commonStore;

  useEffect(() => {
    index()
      .then(getRoles)
      .catch((error) => console.log(error));

    return clearRoles();
  }, [index, getRoles, clearRoles]);

  if (loadingIndex) return null;

  return (
    <Segment loading={loadingIndex} className="principal-segment">
      <RoleHeader />
      <Divider section />
      <RoleTable />
    </Segment>
  );
};

export default observer(Role);
