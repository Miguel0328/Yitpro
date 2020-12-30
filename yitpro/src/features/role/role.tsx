import { observer } from "mobx-react-lite";
import React, { useContext, useEffect } from "react";
import { Divider, Segment } from "semantic-ui-react";
import { RootStoreContext } from "../../app/stores/root";
import RoleHeader from "./RoleHeader";
import RoleTable from "./RoleTable";

const Role = () => {
  const rootStore = useContext(RootStoreContext);
  const { index  } = rootStore.roleStore;
  const { loadingIndex } = rootStore.commonStore;

  useEffect(() => {
    index().catch((error) => console.log(error));
  }, [index]);

  if (loadingIndex) return null;

  return (
    <Segment loading={loadingIndex} className="principal-segment">
      <RoleHeader />
      <Divider section className="principal-divider" />
      <RoleTable />
    </Segment>
  );
};

export default observer(Role);
