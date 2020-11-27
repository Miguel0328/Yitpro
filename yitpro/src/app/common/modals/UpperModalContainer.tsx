import { observer } from "mobx-react-lite";
import React, { useContext } from "react";
import { Icon, Modal } from "semantic-ui-react";
import { RootStoreContext } from "../../stores/rootStore";

const UpperModalContainer = () => {
  const rootStore = useContext(RootStoreContext);
  const {
    upperModal: { open, body, size, header },
    closeUpperModal,
  } = rootStore.modalStore;

  return (
    <Modal
      closeIcon
      centered={false}
      closeOnDimmerClick={false}
      closeOnEscape={false}
      open={open}
      onClose={closeUpperModal}
      size={size}
      className="upper-modal"
    >
      <Modal.Header>
        {header}
        <Icon />
      </Modal.Header>
      <Modal.Content>{body}</Modal.Content>
    </Modal>
  );
};

export default observer(UpperModalContainer);