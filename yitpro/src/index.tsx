import React, { Fragment } from "react";
import ReactDOM from "react-dom";
import * as serviceWorker from "./serviceWorker";
import "react-toastify/dist/ReactToastify.min.css";
import "react-widgets/dist/css/react-widgets.css";
import "semantic-ui-css/semantic.min.css";
import "./app/layout/styles.css";
import 'react-pro-sidebar/dist/css/styles.css';
import App from "./app/layout/App";
import { Router } from "react-router-dom";
import { createBrowserHistory } from "history";
import { library } from "@fortawesome/fontawesome-svg-core";
import {
  faCheckSquare,
  faCoffee,
  faBell,
  faKey,
  faBars,
  faLongArrowAltRight,
  faLongArrowAltLeft
} from "@fortawesome/free-solid-svg-icons";

library.add(faCheckSquare, faCoffee, faBell, faKey, faBars, faLongArrowAltRight, faLongArrowAltLeft);

export const history = createBrowserHistory();

ReactDOM.render(
  <Router history={history}>
    <App />
  </Router>,
  document.getElementById("root")
);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
