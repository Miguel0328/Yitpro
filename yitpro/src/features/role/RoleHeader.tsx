import React, { useContext, useState } from "react";
import { Segment, Header, Button } from "semantic-ui-react";
import { RootStoreContext } from "../../app/stores/rootStore";
import RoleFilter from "./RoleFilter";
import RoleForm from "./RoleForm";

const RoleHeader = () => {
  const rootStore = useContext(RootStoreContext);
  const { setRole } = rootStore.roleStore;
  const { openModal } = rootStore.modalStore;

  const [showFilter, setShowFilter] = useState(false);

  return (
    <Segment clearing className="segment-header">
      <Header
        as="h2"
        style={{ margin: 0 }}
        icon="address card outline"
        className="icon-header"
        content="Roles"
        floated="left"
      />
      <Button
        className="button-filter"
        basic
        floated="right"
        icon="filter"
        onClick={() => setShowFilter(!showFilter)}
      />
      {showFilter && <RoleFilter setShowFilter={setShowFilter} />}
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
