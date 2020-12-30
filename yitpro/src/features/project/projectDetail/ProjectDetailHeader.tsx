import { observer } from "mobx-react-lite";
import React, { useContext } from "react";
import { Header, Segment } from "semantic-ui-react";
import { RootStoreContext } from "../../../app/stores/root";

const ProjectDetailHeader = () => {
  const rootStore = useContext(RootStoreContext);
  const { projectName } = rootStore.projectStore;

  return (
    <Segment clearing className="segment-header">
      <Header
        as="h2"
        style={{ margin: 0 }}
        icon="paperclip"
        className="icon-header"
        content={projectName}
        floated="left"
      />
      {/* <Button
        color="vk"
        content="Editar"
        floated="right"
        onClick={() => {
          getDetail();
          openModal(<ProjectForm />, "small", "Editar: " + projectCode);
        }}
      /> */}
    </Segment>
  );
};

export default observer(ProjectDetailHeader);
