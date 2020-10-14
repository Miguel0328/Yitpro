import React, { Fragment } from "react";
import { ToastContainer } from "react-toastify";
import Login from "../../features/user/Login";
import {
  Route,
  RouteComponentProps,
  Switch,
  withRouter,
} from "react-router-dom";
import { observer } from "mobx-react-lite";
import { Container } from "semantic-ui-react";
import NotFound from "./NotFound";
import Sidebar from "../../features/nav/Sidebar";

const App: React.FC<RouteComponentProps> = ({ location }) => {
  return (
    <Fragment>
      <ToastContainer position="top-right" />
      <Switch>
        <Route exact path="/login" component={Login} />
        <Sidebar />
        <Container>
          <Route component={NotFound} />
        </Container>
      </Switch>
    </Fragment>
  );
};

export default withRouter(observer(App));
