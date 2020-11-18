import { observer } from "mobx-react-lite";
import React, { useContext } from "react";
import { Button, Header, Icon, Modal } from "semantic-ui-react";
import { RootStoreContext } from "../../stores/rootStore";

const DeletionModalContainer = () => {
  const rootStore = useContext(RootStoreContext);
  const {
    deletionModal: { open, name, onDelete },
    closeDeletionModal,
  } = rootStore.modalStore;

  return (
    <Modal
      basic
      closeOnDimmerClick={false}
      closeOnEscape={false}
      onClose={closeDeletionModal}
      open={open}
      size="tiny"
    >
      <Header color="red" icon>
        <Icon name="trash" />
        Eliminar registro
      </Header>
      <Modal.Content>
        <p>
          Esta seguro que desea eliminar el registro{" "}
          <strong style={{ fontSize: "1.2em" }}>{name}</strong>?
        </p>
      </Modal.Content>
      <Modal.Actions>
        <Button basic color="red" inverted onClick={closeDeletionModal}>
          <Icon name="remove" /> No
        </Button>
        <Button color="green" inverted onClick={onDelete}>
          <Icon name="checkmark" /> Yes
        </Button>
      </Modal.Actions>
    </Modal>
  );
};

export default observer(DeletionModalContainer);
