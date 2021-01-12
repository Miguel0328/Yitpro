import React, { useContext } from "react";
import { Grid } from "semantic-ui-react";
import { RootStoreContext } from "../../../app/stores/root";
import ActivityFormDetail from "./ActivityFormDetail";
import ActivityFormTab from "./ActivityFormTab";

const ActivityForm = () => {
  const rootStore = useContext(RootStoreContext);
  const { activityId } = rootStore.activityStore;

  return (
    <Grid stackable className="activity-form no-padding">
      <Grid.Column width={activityId ? 12 : 16}>
        <ActivityFormDetail />
      </Grid.Column>  
      {!!activityId && (
        <Grid.Column width={4} style={{ borderLeft: "solid 1px lightgray" }}>
          <ActivityFormTab />
        </Grid.Column>
      )}
    </Grid>
  );
};

export default ActivityForm;
