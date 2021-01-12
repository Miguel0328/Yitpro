import React, { useContext } from "react";
import { Segment, Header, Button } from "semantic-ui-react";
import { RootStoreContext } from "../../app/stores/root";
import ActivityForm from "./activityForm/ActivityForm";

const ActivityHeader = () => {
  const rootStore = useContext(RootStoreContext);
  const { setActivityId } = rootStore.activityStore;
  const { openModal } = rootStore.modalStore;

  return (
    <Segment clearing className="segment-header">
      <Header
        as="h2"
        style={{ margin: 0 }}
        // icon="pencil"
        image="http://localhost:5000/Files/Icons/lapiz.svg"
        className="icon-header"
        content="Actividades"
        floated="left"
      />
      <Button
        color="vk"
        content="Nuevo"
        floated="right"
        onClick={() => {
          setActivityId(0);
          openModal(<ActivityForm />, "medium", "Nueva actividad");
        }}
      />
    </Segment>
  );
};

export default ActivityHeader;
