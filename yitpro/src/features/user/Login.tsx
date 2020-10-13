import React, { useContext } from "react";
import { Form as FinalForm, Field } from "react-final-form";
import {
  Button,
  Container,
  Form,
  Header,
  Image,
  Segment,
} from "semantic-ui-react";
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
    <Container className="login-container">
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
          <Segment className="login-segment" loading={submitting}>
            <Form onSubmit={handleSubmit} error>
              <Image src="/assets/logo.png" size="small" centered />
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
                color="purple"
                content="Login"
                disabled={(invalid && !dirtySinceLastSubmit) || pristine}
                fluid
              />
            </Form>
          </Segment>
        )}
      />
    </Container>
  );
};

export default Login;
