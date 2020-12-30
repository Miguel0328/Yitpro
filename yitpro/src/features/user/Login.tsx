import React, { useContext } from "react";
import { Form as FinalForm, Field } from "react-final-form";
import {
  Button,
  Container,
  Form,
  Image,
  Segment,
} from "semantic-ui-react";
import { combineValidators, isRequired } from "revalidate";
import { RootStoreContext } from "../../app/stores/root";
import { ILogin } from "../../app/models/profile";
import { FORM_ERROR } from "final-form";
import TextInput from "../../app/common/form/TextInput";
import ErrorMessage from "../../app/common/form/ErrorMessage";

const validate = combineValidators({
  email: isRequired({message: "El correo es obligatorio"}),
  password: isRequired({message: "La contraseña es obligatoria"}),
});

const Login = () => {
  const rootStore = useContext(RootStoreContext);
  const { login } = rootStore.profileStore;

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
                color="facebook"
                content="Login"
                basic
                inverted
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
