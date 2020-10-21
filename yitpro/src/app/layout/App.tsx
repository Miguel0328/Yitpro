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
import "react-pro-sidebar/dist/css/styles.css";
import NotFound from "./NotFound";
import Sidebar from "../../features/nav/Sidebar";
import NavBar from "../../features/nav/Navbar";
import Home from "../../features/home/Home";
import Role from "../../features/role/Role";

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
              <Switch>
                <Route exact path="/" component={Home} />
                <Route exact path="/role" component={Role} />
                <Route component={NotFound} />
              </Switch>
            </Container>
          </Container>
        </Fragment>
      </Switch>
    </Fragment>
  );
};

export default withRouter(observer(App));
