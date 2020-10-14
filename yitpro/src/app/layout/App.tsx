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
import { Container, Segment } from "semantic-ui-react";
import "react-pro-sidebar/dist/css/styles.css";
import NotFound from "./NotFound";
import Sidebar from "../../features/nav/Sidebar";
import NavBar from "../../features/nav/Navbar";

const App: React.FC<RouteComponentProps> = ({ location }) => {
  return (
    <Fragment>
      <ToastContainer position="top-right" />
      <Switch>
        <Route exact path="/login" component={Login} />
        <Fragment>
          <NavBar />
          <Container className="main">
            <Sidebar />
            <Container className="main_content">
              <Route component={NotFound} />
            </Container>
          </Container>
        </Fragment>
      </Switch>
    </Fragment>
  );
};

export default withRouter(observer(App));
