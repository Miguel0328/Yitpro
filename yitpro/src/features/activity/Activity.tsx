import { observer } from "mobx-react-lite";
import React, { useContext, useEffect } from "react";
import { Divider, Segment } from "semantic-ui-react";
import { RootStoreContext } from "../../app/stores/root";
import ActivityHeader from "./ActivityHeader";
import ActivityTable from "./ActivityTable";

const Activity = () => {
  const rootStore = useContext(RootStoreContext);
//   const { index } = rootStore.activityStore;
//   const { loadingIndex } = rootStore.commonStore;

//   useEffect(() => {
//     index().catch((error) => console.log(error));
//   }, [index]);

//   if (loadingIndex) return null;

  return (
    // <Segment loading={loadingIndex} className="principal-segment">
    <Segment className="principal-segment">
      <ActivityHeader />
      <Divider section className="principal-divider" />
      <ActivityTable />
    </Segment>
  );
};

export default observer(Activity);
