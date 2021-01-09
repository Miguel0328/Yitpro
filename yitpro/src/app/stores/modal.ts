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

  @observable.shallow confirmationModal = {
    open: false,
    name: "",
    multiple: false,
    onConfirm: () => {},
    type: "",
    onCancel: () => {},
  };

  @action openModal = (
    content: any,
    size: "mini" | "tiny" | "small" | "medium" | "large" | "fullscreen" | undefined,
    header: string
  ) => {
    if (size === "medium") size = undefined;
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

  @action openConfirmationModal = (
    text: string,
    type: "deletion" | "confirmation",
    onConfirm: () => void,
    multiple?: boolean,
    onCancel?: () => void
  ) => {
    this.confirmationModal.open = true;
    this.confirmationModal.multiple = multiple ?? false;
    this.confirmationModal.name = text;
    this.confirmationModal.onConfirm = onConfirm;
    this.confirmationModal.type = type;
    if (onCancel) this.confirmationModal.onCancel = onCancel;
    else this.confirmationModal.onCancel = () => {};
  };

  @action closeConfirmationModal = () => {
    this.confirmationModal.open = false;
    this.confirmationModal.name = "";
    this.confirmationModal.type = "";
    this.confirmationModal.onConfirm = () => {};
    this.confirmationModal.onCancel = () => {};
  };
}
