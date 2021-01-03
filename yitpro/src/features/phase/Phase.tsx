import { observer } from "mobx-react-lite";
import React from "react";
import { Divider, Grid, Icon, Segment } from "semantic-ui-react";
import ClasificationTable from "./ClasificationTable";
import PhaseHeader from "./PhaseHeader";
import PhaseTable from "./PhaseTable";

const Phase = () => {
  //   const rootStore = useContext(RootStoreContext);
  //   const { index } = rootStore.phaseStore;
  //   const { loadingIndex } = rootStore.commonStore;

  //   useEffect(() => {
  //     index().catch((error) => console.log(error));
  //   }, [index]);

  //   if (loadingIndex) return null;

  return (
    <Segment className="principal-segment">
      <PhaseHeader />
      <Divider section className="principal-divider" />
      {/* <PhaseTable /> */}
      <Segment basic style={{ margin: 0, padding: 0 }}>
        <Grid columns={2}>
          <Grid.Column>
            <PhaseTable />
          </Grid.Column>
          <Grid.Column>
            <ClasificationTable />
          </Grid.Column>
        </Grid>
        <Divider vertical>
          <Icon name="angle double up" />
        </Divider>
      </Segment>
    </Segment>
  );
};

export default observer(Phase);
