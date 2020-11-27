import { observer } from "mobx-react-lite";
import React, { useContext, useEffect } from "react";
import { Divider, Segment } from "semantic-ui-react";
import { RootStoreContext } from "../../app/stores/rootStore";
import UserHeader from "./UserHeader";
import UserTable from "./UserTable";

const User = () => {
  const rootStore = useContext(RootStoreContext);
  const { index, get, clearUsers } = rootStore.userStore;
  const { loadingIndex } = rootStore.commonStore;

  useEffect(() => {
    index()
      .then(get)
      .catch((error) => console.log(error));

    return clearUsers();
  }, [index, get, clearUsers]);

  if (loadingIndex) return null;

  return (
    <Segment loading={loadingIndex} className="principal-segment">
      <UserHeader />
      <Divider section className="principal-divider" />
      <UserTable />
    </Segment>
  );
};

export default observer(User);
