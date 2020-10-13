import React, { Fragment } from "react";
import { ToastContainer } from "react-toastify";
import Login from "../../features/user/Login";

function App() {
  return (
    <Fragment>
      <ToastContainer position="top-right" />
      <Login />
    </Fragment>
  );
}

export default App;
