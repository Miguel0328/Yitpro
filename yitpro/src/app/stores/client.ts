import { observable, action, reaction } from "mobx";
import { toast } from "react-toastify";
import { history } from "../..";
import Client from "../api/client";
import { filterByText, getErrors } from "../common/util/util";
import { Messages } from "../models/messages";
import { IClient, IClientFilter } from "../models/client";
import { RootStore } from "./root";

export default class ClientStore {
  rootStore: RootStore;

  constructor(rootStore: RootStore) {
    this.rootStore = rootStore;

    reaction(
      () => this.clients.slice(),
      () => {
        this.filterClients();
      }
    );
  }

  @observable client: IClient | undefined;
  @observable clients: IClient[] = [];
  @observable filter: IClientFilter = {
    client: "",
    active: "",
  };
  @observable loading = false;
  @observable submitting = false;
  @observable filtered: IClient[] = [];

  getClient = (id: number): IClient | undefined => {
    return this.clients.find((x) => x.id === id);
  };

  @action clearFilter = () => {
    this.filter = { client: "", active: "" };
  };

  @action clearClients = () => {
    this.clients = [];
  };

  @action setClient = (id: number) => {
    let client = this.getClient(id);
    this.client = client;
  };

  @action setFilter = (e: any, result: any = null) => {
    const { name, value } = result || e.target;

    this.filter = {
      ...this.filter,
      [name]: value,
    };
  };

  @action index = async () => {
    this.rootStore.commonStore.loadingIndex = true;
    try {
      await Client.index();
    } catch (error) {
      history.push("/forbidden");
      throw error;
    } finally {
      this.rootStore.commonStore.loadingIndex = false;
    }
  };

  @action get = async () => {
    this.loading = true;
    try {
      const clients = await Client.get();
      this.clients = clients;
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.loading = false;
    }
  };

  @action filterClients = (filterString: string = "") => {
    const keys: Array<keyof IClient> = ["name"];
    this.filtered = this.clients
      .filter(
        (x) =>
          x.active ===
            (this.filter.active === ""
              ? x.active
              : this.filter.active === "yes"
              ? true
              : false) &&
          (x.name.toLowerCase().includes(this.filter.client.toLowerCase()) ||
            this.filter.client === "")
      )
      .filter(filterByText(keys, filterString));
  };

  @action post = async (client: IClient) => {
    this.submitting = true;
    try {
      client.id = await Client.post(client);
      client.projectCount = 0;
      toast.success(Messages.postSuccess);
      this.clients.push(client);
      this.rootStore.modalStore.closeModal();
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.submitting = false;
    }
  };

  @action put = async (client: IClient) => {
    this.submitting = true;
    try {
      await Client.put(client);
      toast.success(Messages.putSuccess);
      const index = this.clients.findIndex((x) => x.id === client.id);
      if (index !== -1) this.clients[index] = client;
      this.rootStore.modalStore.closeModal();
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.submitting = false;
    }
  };

  @action putEnabled = async (client: IClient) => {
    try {
      await Client.put(client);
      toast.success(Messages.putSuccess);
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
      throw error;
    }
  };

  @action download = async () => {
    this.loading = true;
    try {
      console.log("download");
      await Client.download();
    } catch (error) {
      toast.error(getErrors(error));
    } finally {
      this.loading = false;
    }
  };
}
