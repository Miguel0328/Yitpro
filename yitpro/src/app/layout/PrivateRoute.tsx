import { observer } from "mobx-react-lite";
import React, { useContext } from "react";
import {
  Redirect,
  Route,
  RouteComponentProps,
  RouteProps,
} from "react-router-dom";
import { RootStoreContext } from "../stores/root";

interface IProps extends RouteProps {
  component: React.ComponentType<RouteComponentProps<any>>;
}

const PrivateRoute: React.FC<IProps> = ({ component: Component, ...rest }) => {
  const rootStore = useContext(RootStoreContext);
  const { isLoggedIn } = rootStore.profileStore;

  return (
    <Route
      {...rest}
      render={(props) =>
        isLoggedIn ? <Component {...props} /> : <Redirect to={"/login"} />
      }
    />
  );
};

export default observer(PrivateRoute);
