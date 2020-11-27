import React, { useContext } from "react";
import { Button, Form, Grid, Segment } from "semantic-ui-react";
import { Form as FinalForm, Field } from "react-final-form";
import { IRole, RoleFormValues } from "../../app/models/role";
import {
  combineValidators,
  composeValidators,
  hasLengthLessThan,
  isRequired,
} from "revalidate";
import TextInput from "../../app/common/form/TextInput";
import SliderInput from "../../app/common/form/SliderInput";
import { RootStoreContext } from "../../app/stores/rootStore";
import { observer } from "mobx-react-lite";

const validate = combineValidators({
  name: composeValidators(
    isRequired({ message: "El nombre es obligatorio" }),
    hasLengthLessThan(100)({
      message: "La longitud máxima es de 100 carácteres",
    })
  )(),
});

const UserPassword = () => {
  const rootStore = useContext(RootStoreContext);
  const { submitting, role: currentRole, post, put } = rootStore.roleStore;

  let role = new RoleFormValues(currentRole);

  const handleSubmit = async (role: IRole) => {
    if (role.id) {
      put(role);
    } else {
      post(role);
    }
  };

  return (
    <Segment className="form-container" basic loading={submitting}>
      <FinalForm
        initialValues={role}
        onSubmit={handleSubmit}
        validate={validate}
        render={({ handleSubmit, invalid }) => (
          <Form onSubmit={handleSubmit} error>
            <Grid>
              <Grid.Column width={16}>
                <strong>Nombre</strong>
                <Field name="name" placeholder="Nombre" component={TextInput} />
              </Grid.Column>
              <Grid.Column width={16} style={{ paddingTop: 0 }}>
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

export default observer(UserPassword);
