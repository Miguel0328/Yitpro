import React from "react";
import { Form as FinalForm, Field } from "react-final-form";
import { Form, Header } from "semantic-ui-react";
import { combineValidators, isRequired } from "revalidate";

const validate = combineValidators({
  email: isRequired("correo"),
  password: isRequired("contraseña")
})

const Login = () => {
  return (
    <h1>Hola</h1>
  );
};

export default Login;
