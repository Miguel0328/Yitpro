import React, { useContext, useState } from "react";
import { Segment, Header, Button } from "semantic-ui-react";
import { RootStoreContext } from "../../app/stores/rootStore";
import UserFilter from "./UserFilter";
import UserForm from "./UserForm";

const UserHeader = () => {
  const rootStore = useContext(RootStoreContext);
  const { setUserId } = rootStore.userStore;
  const { openModal } = rootStore.modalStore;

  const [showFilter, setShowFilter] = useState(false);

  return (
    <Segment clearing className="segment-header">
      <Header
        as="h2"
        style={{ margin: 0 }}
        icon="users"
        className="icon-header"
        content="Usuarios"
        floated="left"
      />
      <Button
        className="button-filter"
        basic
        floated="right"
        icon="filter"
        onClick={() => setShowFilter(!showFilter)}
      />
      {showFilter && <UserFilter setShowFilter={setShowFilter} />}
      <Button
        color="vk"
        content="Nuevo"
        floated="right"
        onClick={() => {
          setUserId(0);
          openModal(<UserForm />, "small", "Nuevo usuario");
        }}
      />
    </Segment>
  );
};

export default UserHeader;
