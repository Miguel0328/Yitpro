import { observer } from "mobx-react-lite";
import React, { useContext } from "react";
import { Button, Header, Icon, Modal } from "semantic-ui-react";
import { RootStoreContext } from "../../stores/root";

const ConfirmationModalContainer = () => {
  const rootStore = useContext(RootStoreContext);
  const {
    confirmationModal: { open, name, type, multiple, onConfirm, onCancel },
    closeConfirmationModal,
  } = rootStore.modalStore;

  return (
    <Modal
      basic
      closeOnDimmerClick={false}
      closeOnEscape={false}
      onClose={closeConfirmationModal}
      open={open}
      size="tiny"
    >
      {type === "deletion" ? (
        <Header icon>
          <Icon color="red" name="trash" />
          Eliminar
        </Header>
      ) : (
        <Header icon>
          <Icon color="blue" name="question" />
          Confirmar
        </Header>
      )}
      <Modal.Content>
        {multiple ? (
          <p>
            Esta seguro que desea {type === "deletion" ? "eliminar" : "agregar"}{" "}
            <strong style={{ fontSize: "1.1em" }}>{name}</strong> registro(s)?
          </p>
        ) : (
          <p>
            Esta seguro que desea {type === "deletion" ? "eliminar" : "agregar"}{" "}
            el registro <strong style={{ fontSize: "1.1em" }}>{name}</strong>?
          </p>
        )}
      </Modal.Content>
      <Modal.Actions>
        <Button
          color="red"
          inverted
          onClick={() => {
            closeConfirmationModal();
            onCancel();
          }}
        >
          <Icon name="remove" /> No
        </Button>
        <Button
          color="green"
          inverted
          onClick={() => {
            closeConfirmationModal();
            onConfirm();
          }}
        >
          <Icon name="checkmark" /> Sí
        </Button>
      </Modal.Actions>
    </Modal>
  );
};

export default observer(ConfirmationModalContainer);
