import React, { useContext } from "react";
import { Segment, Header, Button } from "semantic-ui-react";
import { RootStoreContext } from "../../app/stores/root";
import ProjectForm from "./ProjectForm";

const ProjectHeader = () => {
  const rootStore = useContext(RootStoreContext);
  const { setProjectId } = rootStore.projectStore;
  const { openModal } = rootStore.modalStore;

  return (
    <Segment clearing className="segment-header">
      <Header
        as="h2"
        style={{ margin: 0 }}
        icon="paperclip"
        className="icon-header"
        content="Proyectos"
        floated="left"
      />
      <Button
        color="vk"
        content="Nuevo"
        floated="right"
        onClick={() => {
          setProjectId(0);
          openModal(<ProjectForm />, "small", "Nuevo proyecto");
        }}
      />
    </Segment>
  );
};

export default ProjectHeader;
