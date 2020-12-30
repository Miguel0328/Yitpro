import { action, observable } from "mobx";
import { Modal } from "semantic-ui-react";
import { RootStore } from "./root";

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

  @observable.shallow upperModal = {
    open: false,
    body: null,
    size: Modal.defaultProps!.size,
    header: "",
  };

  @observable.shallow deletionModal = {
    open: false,
    name: "",
    multiple: false,
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

  @action openUpperModal = (content: any, size: any, header: string) => {
    this.upperModal.open = true;
    this.upperModal.body = content;
    this.upperModal.size = size;
    this.upperModal.header = header;
  };

  @action closeUpperModal = () => {
    this.upperModal.open = false;
    this.upperModal.body = null;
    this.upperModal.header = "";
  };

  @action openDeletionModal = (
    name: string,
    onDelete: () => void,
    multiple?: boolean
  ) => {
    this.deletionModal.open = true;
    this.deletionModal.multiple = multiple ?? false;
    this.deletionModal.name = name;
    this.deletionModal.onDelete = onDelete;
  };

  @action closeDeletionModal = () => {
    this.deletionModal.open = false;
    this.deletionModal.name = "";
    this.deletionModal.onDelete = () => {};
  };
}
