import React, { useContext } from "react";
import { Button, Form, Grid, Segment } from "semantic-ui-react";
import { Form as FinalForm, Field } from "react-final-form";
import { ICatalog, CatalogFormValues } from "../../app/models/catalog";
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
import TextAreaInput from "../../app/common/form/TextAreaInput";

const validate = combineValidators({
  alias: composeValidators(
    isRequired({ message: "El alias es obligatorio" }),
    hasLengthLessThan(100)({
      message: "La longitud máxima es de 100 carácteres",
    })
  )(),
  description: composeValidators(
    isRequired({ message: "La descripción es obligatorio" }),
    hasLengthLessThan(500)({
      message: "La longitud máxima es de 500 carácteres",
    })
  )(),
});

const CatalogForm = () => {
  const rootStore = useContext(RootStoreContext);
  const {
    submitting,
    catalog: currentCatalog,
    catalogId,
    post,
    put,
  } = rootStore.catalogStore;

  let catalog = new CatalogFormValues(currentCatalog);

  const handleSubmit = async (catalog: ICatalog) => {
    catalog.catalogId = Number(catalogId);
    if (catalog.id) {
      put(catalog);
    } else {
      post(catalog);
    }
  };

  return (
    <Segment className="form-container" basic loading={submitting}>
      <FinalForm
        initialValues={catalog}
        onSubmit={handleSubmit}
        validate={validate}
        render={({ handleSubmit, invalid }) => (
          <Form onSubmit={handleSubmit} error>
            <Grid>
              <Grid.Column width={16}>
                <strong>Alias</strong>
                <Field name="alias" placeholder="Alias" component={TextInput} />
              </Grid.Column>
              <Grid.Column width={16}>
                <strong>Descripción</strong>
                <Field
                  name="description"
                  placeholder="Descripción"
                  rows="2"
                  component={TextAreaInput}
                />
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

export default observer(CatalogForm);
