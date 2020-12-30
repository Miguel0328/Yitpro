import { observer } from "mobx-react-lite";
import React, { useContext, useEffect } from "react";
import { Divider, Segment } from "semantic-ui-react";
import { RootStoreContext } from "../../app/stores/root";
import ProjectHeader from "./ProjectHeader";
import ProjectTable from "./ProjectTable";

const Project = () => {
  const rootStore = useContext(RootStoreContext);
  const { index } = rootStore.projectStore;
  const { loadingIndex } = rootStore.commonStore;

  useEffect(() => {
    index().catch((error) => console.log(error));
  }, [index]);

  if (loadingIndex) return null;

  return (
    <Segment loading={loadingIndex} className="principal-segment">
      <ProjectHeader />
      <Divider section className="principal-divider" />
      <ProjectTable />
    </Segment>
  );
};

export default observer(Project);
