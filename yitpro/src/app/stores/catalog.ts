import { observable, action, reaction } from "mobx";
import { toast } from "react-toastify";
import { history } from "../..";
import Catalog from "../api/catalog";
import { filterByText, getErrors } from "../common/util/util";
import { Messages } from "../models/messages";
import {
  CatalogFilterValues,
  ICatalog,
  ICatalogFilter,
} from "../models/catalog";
import { RootStore } from "./root";

export default class CatalogStore {
  rootStore: RootStore;

  constructor(rootStore: RootStore) {
    this.rootStore = rootStore;

    reaction(
      () => this.catalogs.slice(),
      () => {
        this.filterCatalogs();
      }
    );
  }

  @observable catalog: ICatalog | undefined;
  @observable catalogId: number | "" = "";
  @observable catalogs: ICatalog[] = [];
  @observable filter: ICatalogFilter = new CatalogFilterValues();
  @observable loading = false;
  @observable submitting = false;
  @observable filtered: ICatalog[] = [];

  getCatalog = (id: number): ICatalog | undefined => {
    return this.catalogs.find((x) => x.id === id);
  };

  @action clearFilter = () => {
    this.filter = new CatalogFilterValues();
  };

  @action clearCatalogs = () => {
    this.catalogs = [];
  };

  @action clearCatalogId = () => {
    this.catalogId = "";
  };

  @action setCatalogId = (e: any, result: any = null) => {
    const { value } = result || e.target;
    this.catalogId = value;
  };

  @action setCatalog = (id: number) => {
    let catalog = this.getCatalog(id);
    this.catalog = catalog;
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
      await Catalog.index();
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
      let catalogs: ICatalog[] = [];
      if (this.catalogId !== "") catalogs = await Catalog.get(this.catalogId);
      this.catalogs = catalogs;
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.loading = false;
    }
  };

  @action filterCatalogs = (filterString: string = "") => {
    const keys: Array<keyof ICatalog> = [
      "alias",
      "description",
      "active",
      "protected",
    ];
    this.filtered = this.catalogs
      .filter(
        (x) =>
          x.active ===
            (this.filter.active === ""
              ? x.active
              : this.filter.active === "yes"
              ? true
              : false) &&
          (x.alias.toLowerCase().includes(this.filter.alias.toLowerCase()) ||
            this.filter.alias === "") &&
          (x.description
            .toLowerCase()
            .includes(this.filter.description.toLowerCase()) ||
            this.filter.description === "")
      )
      .filter(filterByText(keys, filterString));
  };

  @action post = async (catalog: ICatalog) => {
    this.submitting = true;
    try {
      catalog.id = await Catalog.post(catalog);
      toast.success(Messages.postSuccess);
      this.catalogs.push(catalog);
      this.rootStore.modalStore.closeModal();
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.submitting = false;
    }
  };

  @action put = async (catalog: ICatalog) => {
    this.submitting = true;
    try {
      await Catalog.put(catalog);
      toast.success(Messages.putSuccess);
      const index = this.catalogs.findIndex((x) => x.id === catalog.id);
      if (index !== -1) this.catalogs[index] = catalog;
      this.rootStore.modalStore.closeModal();
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
    } finally {
      this.submitting = false;
    }
  };

  @action putEnabled = async (catalog: ICatalog) => {
    try {
      await Catalog.put(catalog);
      toast.success(Messages.putSuccess);
    } catch (error) {
      if (error && error?.status !== 500) toast.error(getErrors(error));
      throw error;
    }
  };

  @action download = async () => {
    this.loading = true;
    try {
      await Catalog.download();
    } catch (error) {
      toast.error(getErrors(error));
    } finally {
      this.loading = false;
    }
  };
}
