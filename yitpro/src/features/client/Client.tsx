import { observer } from "mobx-react-lite";
import React, { useContext, useEffect } from "react";
import { Divider, Segment } from "semantic-ui-react";
import { RootStoreContext } from "../../app/stores/root";
import ClientHeader from "./ClientHeader";
import ClientTable from "./ClientTable";

const Client = () => {
  const rootStore = useContext(RootStoreContext);
  const { index } = rootStore.clientStore;
  const { loadingIndex } = rootStore.commonStore;

  useEffect(() => {
    index().catch((error) => console.log(error));
  }, [index]);

  if (loadingIndex) return null;

  return (
    <Segment className="principal-segment">
      <ClientHeader />
      <Divider section className="principal-divider" />
      <ClientTable />
    </Segment>
  );
};

export default observer(Client);
