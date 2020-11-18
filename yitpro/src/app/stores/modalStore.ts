import { action, observable } from "mobx";
import { Modal } from "semantic-ui-react";
import { RootStore } from "./rootStore";

export default class ModalStore {
  rootStore: RootStore;
  constructor(rootStore: RootStore) {
    this.rootStore = rootStore;
  }

  @observable.shallow modal = {
    open: false,
    body: null,
    size: Modal.defaultProps!.size,
    header: "",
  };

  @observable.shallow deletionModal = {
    open: false,
    name: "",
    onDelete: () => {},
  };

  @action openModal = (content: any, size: any, header: string) => {
    this.modal.open = true;
    this.modal.body = content;
    this.modal.size = size;
    this.modal.header = header;
  };

  @action closeModal = () => {
    this.modal.open = false;
    this.modal.body = null;
    this.modal.header = "";
  };

  @action openDeletionModal = (
    name: string,
    onDelete: () => void
  ) => {
    this.deletionModal.open = true;
    this.deletionModal.name = name;
    this.deletionModal.onDelete = onDelete;
  };

  @action closeDeletionModal = () => {
    this.deletionModal.open = false;
    this.deletionModal.name = "";
    this.deletionModal.onDelete = () => {};
  };
}
