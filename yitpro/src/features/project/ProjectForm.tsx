import { observer } from "mobx-react-lite";
import React, { useContext, useEffect } from "react";
import { Form as FinalForm, Field } from "react-final-form";
import {
  combineValidators,
  composeValidators,
  isRequired,
  hasLengthLessThan,
} from "revalidate";
import {
  Form,
  Button,
  Grid,
  Segment,
} from "semantic-ui-react";
import SelectInput from "../../app/common/form/SelectInput";
import SliderInput from "../../app/common/form/SliderInput";
import TextAreaInput from "../../app/common/form/TextAreaInput";
import TextInput from "../../app/common/form/TextInput";
import { IProjectDetail, ProjectFormValues } from "../../app/models/project";
import { RootStoreContext } from "../../app/stores/root";

const validate = combineValidators({
  name: composeValidators(
    isRequired({ message: "El nombre es obligatorio" }),
    hasLengthLessThan(200)({
      message: "La longitud máxima es de 200 carácteres",
    })
  )(),
  code: composeValidators(
    isRequired({ message: "La clave es obligatoria" }),
    hasLengthLessThan(20)({
      message: "La longitud máxima es de 20 carácteres",
    })
  )(),
  description: composeValidators(
    isRequired({ message: "La descripción es obligatoria" }),
    hasLengthLessThan(1200)({
      message: "La longitud máxima es de 1200 carácteres",
    })
  )(),
  leaderId: isRequired({ message: "El líder es obligatorio" }),
  clientId: isRequired({ message: "El cliente es obligatorio" }),
});

const ProjectForm = () => {
  const rootStore = useContext(RootStoreContext);
  const {
    projectId,
    submitting,
    project: currentProject,
    initForm,
    getDetail,
    post,
    put,
    clearProject,
  } = rootStore.projectStore;
  const { clientOptions, lineManagersOptions } = rootStore.optionStore;

  useEffect(() => {
    initForm();
    if (projectId !== 0) {
      getDetail();
    }

    return () => {
      clearProject();
    };
  }, [projectId, initForm, clearProject, getDetail]);

  let project = new ProjectFormValues(currentProject);

  const handleSubmit = async (project: IProjectDetail) => {
    if (project.id) {
      put(project);
    } else {
      post(project);
    }
  };

  return (
    <Segment className="form-container" basic loading={submitting}>
      <FinalForm
        initialValues={project}
        onSubmit={handleSubmit}
        validate={validate}
        render={({ handleSubmit, invalid }) => {
          return (
            <Form onSubmit={handleSubmit} error>
              <Grid stackable>
                <Grid.Column style={{ width: "66.66%" }}>
                  <strong>Nombre</strong>
                  <Field
                    name="name"
                    placeholder="Nombre"
                    component={TextInput}
                  />
                </Grid.Column>
                <Grid.Column style={{ width: "33.33%" }}>
                  <strong>Clave</strong>
                  <Field
                    name="code"
                    placeholder="Clave"
                    component={TextInput}
                  />
                </Grid.Column>
              </Grid>
              <Grid stackable columns={3}>
                <Grid.Column>
                  <strong>Cliente</strong>
                  <Field
                    name="clientId"
                    placeholder="Cliente"
                    options={clientOptions ?? []}
                    component={SelectInput}
                  />
                </Grid.Column>
                <Grid.Column>
                  <strong>Líder</strong>
                  <Field
                    name="leaderId"
                    placeholder="Líder"
                    options={lineManagersOptions ?? []}
                    component={SelectInput}
                  />
                </Grid.Column>
                {/* <Grid.Column>
                  <strong>Metodología</strong>
                  <Field
                    name="managerId"
                    placeholder="Estatus"
                    options={lineManagersOptions ?? []}
                    component={SelectInput}
                  />
                </Grid.Column> */}
              </Grid>
              {/* <Grid stackable columns={3}>
                <Grid.Column>
                  <strong>Tipo de proyecto</strong>
                  <Field
                    name="managerId"
                    placeholder="Estatus"
                    options={lineManagersOptions ?? []}
                    component={SelectInput}
                  />
                </Grid.Column>
                <Grid.Column>
                  <strong>Estatus</strong>
                  <Field
                    name="managerId"
                    placeholder="Estatus"
                    options={lineManagersOptions ?? []}
                    component={SelectInput}
                  />
                </Grid.Column>
              </Grid> */}
              <Grid stackable>
                <Grid.Column width={16}>
                  <strong>Descripción</strong>
                  <Field
                    name="description"
                    rows={2}
                    component={TextAreaInput}
                    type="checkbox"
                  />
                </Grid.Column>
                <Grid.Column width={2}>
                  <strong>Activo</strong>
                  <Field
                    name="active"
                    component={SliderInput}
                    type="checkbox"
                  />
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
          );
        }}
      />
    </Segment>
  );
};

export default observer(ProjectForm);
