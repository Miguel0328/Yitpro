import React, { useContext } from "react";
import { Form as FinalForm, Field } from "react-final-form";
import { Button, Form, Header } from "semantic-ui-react";
import { combineValidators, isRequired } from "revalidate";
import { RootStoreContext } from "../../app/stores/rootStore";
import { ILogin } from "../../app/models/user";
import { FORM_ERROR } from "final-form";
import TextInput from "../../app/common/form/TextInput";
import ErrorMessage from "../../app/common/form/ErrorMessage";

const validate = combineValidators({
  email: isRequired("correo"),
  password: isRequired("contraseña"),
});

const Login = () => {
  const rootStore = useContext(RootStoreContext);
  const { login } = rootStore.userStore;

  return (
      <FinalForm
        onSubmit={(values: ILogin) =>
          login(values).catch((error) => ({
            [FORM_ERROR]: error,
          }))
        } 
        validate={validate}
        render={({
          handleSubmit,
          submitting,
          submitError,
          invalid,
          pristine,
          dirtySinceLastSubmit,
        }) => (
          <Form onSubmit={handleSubmit} error>
            <Header as="h2" content="Login" color="teal" textAlign="center" />
            <Field name="email" placeholder="Correo" component={TextInput} />
            <Field
              name="password"
              placeholder="Contraseña"
              component={TextInput}
              type="password"
            />
            {submitError && !dirtySinceLastSubmit && !submitting && (
              <ErrorMessage
                error={submitError}
                text="Credenciales incorrectas"
              />
            )}
            <Button
              loading={submitting}
              color="teal"
              content="Login"
              disabled={(invalid && !dirtySinceLastSubmit) || pristine}
              fluid
            />
          </Form>
        )}
      />
  );
};

export default Login;
