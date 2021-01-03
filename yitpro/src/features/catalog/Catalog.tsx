import { observer } from "mobx-react-lite";
import React, { useContext, useEffect } from "react";
import { Divider, Segment } from "semantic-ui-react";
import { RootStoreContext } from "../../app/stores/root";
import CatalogHeader from "./CatalogHeader";
import CatalogTable from "./CatalogTable";
// import CatalogHeader from "./CatalogHeader";
// import CatalogTable from "./CatalogTable";

const Catalog = () => {
  const rootStore = useContext(RootStoreContext);
   const { index  } = rootStore.catalogStore;
  const { loadingIndex } = rootStore.commonStore;

  useEffect(() => {
    index().catch((error) => console.log(error));
  }, [index]);

  if (loadingIndex) return null;

  return (
    <Segment loading={loadingIndex} className="principal-segment">
      <CatalogHeader />
      <Divider section className="principal-divider" />
      <CatalogTable />
    </Segment>
  );
};

export default observer(Catalog);
