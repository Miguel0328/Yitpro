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

const App: React.FC<RouteComponentProps> = ({ location }) => {
  return (
    <Fragment>
      <ToastContainer position="top-right" />
      <Route exact path="/login" component={Login} />
    </Fragment>
  );
};

export default withRouter(observer(App));
