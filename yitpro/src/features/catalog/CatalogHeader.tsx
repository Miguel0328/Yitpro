import { observer } from "mobx-react-lite";
import React, { useContext, useEffect } from "react";
import {
  Segment,
  Header,
  Button,
  Dropdown,
} from "semantic-ui-react";
import { RootStoreContext } from "../../app/stores/root";
import CatalogForm from "./CatalogForm";

const CatalogHeader = () => {
  const rootStore = useContext(RootStoreContext);
  const {
    catalogId,
    get,
    setCatalog,
    setCatalogId,
    clearCatalogId,
  } = rootStore.catalogStore;
  const { openModal } = rootStore.modalStore;
  const {
    catalogOptions,
    loadingCatalogs,
    getCatalogOptions,
  } = rootStore.optionStore;

  useEffect(() => {
    getCatalogOptions();

    return () => {
      clearCatalogId();
    };
  }, [getCatalogOptions, clearCatalogId]);

  return (
    <Segment clearing className="segment-header">
      <Header
        as="h2"
        style={{ margin: 0 }}
        icon="folder open"
        className="icon-header"
        content="Catálogos"
        floated="left"
      />
      <Button
        color="vk"
        content="Nuevo"
        floated="right"
        disabled={catalogId === ""}
        onClick={() => {
          setCatalog(0);
          openModal(<CatalogForm />, "tiny", "Nuevo catálogo");
        }}
      />
      <Dropdown
        className="right floated"
        disabled={loadingCatalogs}
        loading={loadingCatalogs}
        search
        clearable
        selection
        pointing="top right"
        name="roleId"
        placeholder="Catálogo..."
        value={catalogId}
        onChange={(e, result) => {
          setCatalogId(e, result);
          get();
        }}
        options={catalogOptions}
      />
    </Segment>
  );
};

export default observer(CatalogHeader);
