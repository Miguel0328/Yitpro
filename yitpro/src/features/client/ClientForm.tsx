import React, { useContext } from "react";
import { Button, Form, Grid, Segment } from "semantic-ui-react";
import { Form as FinalForm, Field } from "react-final-form";
import { IClient, ClientFormValues } from "../../app/models/client";
import {
  combineValidators,
  composeValidators,
  hasLengthLessThan,
  isRequired,
} from "revalidate";
import TextInput from "../../app/common/form/TextInput";
import SliderInput from "../../app/common/form/SliderInput";
import { RootStoreContext } from "../../app/stores/root";
import { observer } from "mobx-react-lite";

const validate = combineValidators({
  name: composeValidators(
    isRequired({ message: "El nombre es obligatorio" }),
    hasLengthLessThan(300)({
      message: "La longitud máxima es de 300 carácteres",
    })
  )(),
});

const ClientForm = () => {
  const rootStore = useContext(RootStoreContext);
  const { submitting, client: currentClient, post, put } = rootStore.clientStore;

  let client = new ClientFormValues(currentClient);

  const handleSubmit = async (client: IClient) => {
    console.log("Hola")
    if (client.id) {
      put(client);
    } else {
      post(client);
    }
  };

  return (
    <Segment className="form-container" basic loading={submitting}>
      <FinalForm
        initialValues={client}
        onSubmit={handleSubmit}
        validate={validate}
        render={({ handleSubmit, invalid }) => (
          <Form onSubmit={handleSubmit} error>
            <Grid>
              <Grid.Column width={16}>
                <strong>Nombre</strong>
                <Field name="name" placeholder="Nombre" component={TextInput} />
              </Grid.Column>
              <Grid.Column width={16}>
                <strong>Activo</strong>
                <Field name="active" component={SliderInput} type="checkbox" />
              </Grid.Column>
              <Grid.Column textAlign="right" width={16}>
                <Button
                  color="vk"
                  content="Guardar"
                  disabled={submitting || invalid}
                />
              </Grid.Column>
            </Grid>
          </Form>
        )}
      />
    </Segment>
  );
};

export default observer(ClientForm);
