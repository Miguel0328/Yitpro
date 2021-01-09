import { observer } from "mobx-react-lite";
import React, { useContext } from "react";
import { Icon, Modal } from "semantic-ui-react";
import { RootStoreContext } from "../../stores/root";

const ModalContainer = () => {
  const rootStore = useContext(RootStoreContext);
  const {
    modal: { open, body, size, header },
    closeModal,
  } = rootStore.modalStore;
  
  return (
    <Modal
      closeIcon
      centered={false}
      closeOnDimmerClick={false}
      closeOnEscape={false}
      open={open}
      onClose={closeModal}
      size={size}
    >
      <Modal.Header>
        {header}
        <Icon />
      </Modal.Header>
      <Modal.Content>{body}</Modal.Content>
    </Modal>
  );
};

export default observer(ModalContainer);
