import React, { useContext } from "react";
import { Button, Container, Form, Grid } from "semantic-ui-react";
import { Form as FinalForm, Field } from "react-final-form";
import { IRole, RoleFormValues } from "../../app/models/role";
import { combineValidators, composeValidators, hasLengthLessThan, isRequired } from "revalidate";
import TextInput from "../../app/common/form/TextInput";
import SliderInput from "../../app/common/form/SliderInput";
import { RootStoreContext } from "../../app/stores/rootStore";
import { observer } from "mobx-react-lite";

const validate = combineValidators({
  name: composeValidators(
    isRequired({ message: "El nombre es obligatorio" }),
    hasLengthLessThan(100)({
      message: "La longitud máxima es de 100 carácteres"
    })
  )()
});

const RoleForm = () => {
  const rootStore = useContext(RootStoreContext);
  const { submitting, role: currentRole, postRole, putRole } = rootStore.roleStore;

  let role = new RoleFormValues(currentRole);

  const handleSubmit = async (role: IRole) => {
    if (role.id) {
      putRole(role);
    } else {
      postRole(role);
    }
  };

  return (
    <Container>
      <FinalForm
        initialValues={role}
        onSubmit={handleSubmit}
        validate={validate}
        render={({
          handleSubmit,
          invalid,
          pristine
        }) => (
          <Form onSubmit={handleSubmit} error>
            <Grid>
              <Grid.Column width={3} verticalAlign="middle">
                <label>Nombre</label>
              </Grid.Column>
              <Grid.Column width={13}>
                <Field
                  name="name"
                  placeholder="Nombre"
                  component={TextInput}
                />
              </Grid.Column>
              <Grid.Column width={3} verticalAlign="middle">
                <label>Activo</label>
              </Grid.Column>
              <Grid.Column width={13}>
                <Field name="active" component={SliderInput} type="checkbox" />
              </Grid.Column>
              <Grid.Column textAlign="right" width={16}>
                <Button
                  color="vk"
                  content="Guardar"
                  disabled={submitting || invalid || pristine}
                  loading={submitting}
                />
              </Grid.Column>
            </Grid>
          </Form>
        )}
      />
    </Container>
  );
};

export default observer(RoleForm);
