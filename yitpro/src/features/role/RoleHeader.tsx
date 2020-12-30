import React, { useContext } from "react";
import { Segment, Header, Button } from "semantic-ui-react";
import { RootStoreContext } from "../../app/stores/root";
import RoleForm from "./RoleForm";

const RoleHeader = () => {
  const rootStore = useContext(RootStoreContext);
  const { setRole } = rootStore.roleStore;
  const { openModal } = rootStore.modalStore;

  return (
    <Segment clearing className="segment-header">
      <Header
        as="h2"
        style={{ margin: 0 }}
        icon="address card"
        className="icon-header"
        content="Roles"
        floated="left"
      />
      <Button
        color="vk"
        content="Nuevo"
        floated="right"
        onClick={() => {
          setRole(0);
          openModal(<RoleForm />, "tiny", "Nuevo rol");
        }}
      />
    </Segment>
  );
};

export default RoleHeader;
