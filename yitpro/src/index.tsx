import React, { Fragment } from 'react';
import ReactDOM from 'react-dom';
import * as serviceWorker from './serviceWorker';
import "react-toastify/dist/ReactToastify.min.css";
import "react-widgets/dist/css/react-widgets.css";
import 'semantic-ui-css/semantic.min.css'
import "./app/layout/styles.css";
import App from './app/layout/App';

ReactDOM.render(
  <Fragment>
    <App />
  </Fragment>,
  document.getElementById('root')
);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
