import React, { Fragment, useContext, useEffect } from "react";
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
import ModalContainer from "../common/modals/ModalContainer";
import DeletionModalContainer from "../common/modals/DeletionModalContainer";
import PrivateRoute from "./PrivateRoute";
import { RootStoreContext } from "../stores/root";
import LoadingComponent from "./LoadingComponent";
import Forbidden from "./Forbidden";
import User from "../../features/user/User";
import UpperModalContainer from "../common/modals/UpperModalContainer";
import CalendarComponent from "../../features/calendar/Calendar";
import Client from "../../features/client/Client";
import Project from "../../features/project/Project";
import ProjectDetail from "../../features/project/projectDetail/ProjectDetail";

const App: React.FC<RouteComponentProps> = ({ location }) => {
  const rootStore = useContext(RootStoreContext);
  const { token, appLoaded, setAppLoaded } = rootStore.commonStore;
  const { current } = rootStore.profileStore;

  useEffect(() => {
    if (token) {
      current().finally(() => setAppLoaded());
    } else {
      setAppLoaded();
    }
  }, [token, current, setAppLoaded]);

  if (!appLoaded) return <LoadingComponent content="Cargando..." />;

  return (
    <Fragment>
      <ToastContainer position="top-right" />
      <ModalContainer />
      <UpperModalContainer />
      <DeletionModalContainer />
      <Switch>
        <Route exact path="/login" component={Login} />
        <Fragment>
          <NavBar />
          <Container className="main">
            <Sidebar />
            <Container className="main_content">
              <Switch>
                <PrivateRoute exact path="/" component={Home} />
                <PrivateRoute exact path="/role" component={Role} />
                <PrivateRoute exact path="/user" component={User} />
                <PrivateRoute exact path="/client" component={Client} />
                <PrivateRoute exact path="/project" component={Project} />
                <PrivateRoute
                  exact
                  path="/project/detail/:code"
                  component={ProjectDetail}
                />
                <PrivateRoute
                  exact
                  path="/calendar"
                  component={CalendarComponent}
                />
                <PrivateRoute exact path="/forbidden" component={Forbidden} />
                <PrivateRoute component={NotFound} />
              </Switch>
            </Container>
          </Container>
        </Fragment>
      </Switch>
    </Fragment>
  );
};

export default withRouter(observer(App));
