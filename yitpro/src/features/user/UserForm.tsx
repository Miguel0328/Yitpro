import { observer } from "mobx-react-lite";
import React, { useContext, useEffect, useState } from "react";
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
  Container,
  Image,
  Segment,
} from "semantic-ui-react";
import DateInput from "../../app/common/form/DateInput";
import SelectInput from "../../app/common/form/SelectInput";
import SliderInput from "../../app/common/form/SliderInput";
import TextInput from "../../app/common/form/TextInput";
import PhotoUploadWidget from "../../app/common/photoUpload/PhotoUploadWidget";
import { IUserDetails, UserFormValues } from "../../app/models/user";
import { RootStoreContext } from "../../app/stores/rootStore";

const validate = combineValidators({
  employeeNumber: composeValidators(
    isRequired({ message: "El numero de empleado es obligatorio" }),
    hasLengthLessThan(50)({
      message: "La longitud máxima es de 50 carácteres",
    })
  )(),
  firstName: composeValidators(
    isRequired({ message: "El nombre es obligatorio" }),
    hasLengthLessThan(50)({
      message: "La longitud máxima es de 100 carácteres",
    })
  )(),
  lastName: composeValidators(
    isRequired({ message: "El apellido paterno es obligatorio" }),
    hasLengthLessThan(50)({
      message: "La longitud máxima es de 100 carácteres",
    })
  )(),
  secondLastName: composeValidators(
    isRequired({ message: "El apellido materno es obligatorio" }),
    hasLengthLessThan(50)({
      message: "La longitud máxima es de 100 carácteres",
    })
  )(),
  email: composeValidators(
    isRequired({ message: "El correo es obligatorio" }),
    hasLengthLessThan(50)({
      message: "La longitud máxima es de 200 carácteres",
    })
  )(),
  admissionDate: isRequired({ message: "La fecha de ingreso es obligatoria" }),
  roleId: isRequired({ message: "El rol es obligatorio" }),
});

const UserForm = () => {
  const rootStore = useContext(RootStoreContext);
  const {
    userId,
    submitting,
    user: currentUser,
    initForm,
    getById,
    post,
    put,
    clearUser,
  } = rootStore.userStore;
  const { openUpperModal } = rootStore.modalStore;
  const { roleOptions, lineManagersOptions } = rootStore.optionStore;

  const [photo, setPhoto] = useState<Blob>();

  useEffect(() => {
    initForm();
    if (userId !== 0) {
      getById();
    }

    return clearUser();
  }, [userId, initForm, clearUser, getById]);

  let user = new UserFormValues(currentUser);

  const handleSubmit = async (user: IUserDetails) => {
    let formData = new FormData();
    formData.append("id", user.id.toString());
    formData.append("employeeNumber", user.employeeNumber);
    formData.append("firstName", user.firstName);
    formData.append("lastName", user.lastName);
    formData.append("secondLastName", user.secondLastName);
    formData.append("email", user.email);
    formData.append("admissionDate", user.admissionDate?.toUTCString() ?? "");
    if (user.roleId) formData.append("roleId", user.roleId.toString());
    if (user.managerId) formData.append("managerId", user.managerId.toString());
    formData.append("active", user.active.toString());
    formData.append("locked", user.locked.toString());
    formData.append("photo", photo ?? "");

    if (user.id) {
      put(formData);
    } else {
      post(formData);
    }
  };

  return (
    <Segment className="form-container" basic loading={submitting}>
      <FinalForm
        initialValues={user}
        onSubmit={handleSubmit}
        validate={validate}
        render={({ handleSubmit, invalid }) => (
          <Form onSubmit={handleSubmit} error>
            <Grid>
              <Grid.Row stretched style={{ paddingBottom: 0 }}>
                <Grid.Column width={4}>
                  <Image
                    circular
                    style={{ width: "100%" }}
                    src={
                      photo
                        ? URL.createObjectURL(photo)
                        : user.photoUrl
                        ? user.photoUrl
                        : "assets/avatar.png"
                    }
                  />
                </Grid.Column>
                <Grid.Column width={6}>
                  <Container style={{ margin: 0, padding: 0 }}>
                    <strong>Numero de empleado</strong>
                    <Field
                      name="employeeNumber"
                      placeholder="Numero de empleado"
                      component={TextInput}
                    />
                  </Container>
                  <Container>
                    <strong>Apellido paterno</strong>
                    <Field
                      name="lastName"
                      placeholder="Apellido paterno"
                      component={TextInput}
                    />
                  </Container>
                </Grid.Column>
                <Grid.Column width={6}>
                  <Container style={{ margin: 0, padding: 0 }}>
                    <strong>Nombre(s)</strong>
                    <Field
                      name="firstName"
                      placeholder="Nombre(s)"
                      component={TextInput}
                    />
                  </Container>
                  <Container>
                    <strong>Apellido materno</strong>
                    <Field
                      name="secondLastName"
                      placeholder="Apellido materno"
                      component={TextInput}
                    />
                  </Container>
                </Grid.Column>
              </Grid.Row>
              <Grid.Column width={4} verticalAlign="bottom">
                <Button
                  fluid
                  onClick={(e) => {
                    e.preventDefault();
                    openUpperModal(
                      <PhotoUploadWidget setPhoto={setPhoto} />,
                      "tiny",
                      "Foto"
                    );
                  }}
                >
                  Agregar Foto
                </Button>
              </Grid.Column>
              <Grid.Column width={6}>
                <Container>
                  <strong>Correo</strong>
                  <Field
                    name="email"
                    placeholder="Correo"
                    component={TextInput}
                  />
                </Container>
              </Grid.Column>
              <Grid.Column width={6}>
                <strong>Fecha de ingreso</strong>
                <Field
                  name="admissionDate"
                  placeholder="Fecha de ingreso"
                  date={true}
                  component={DateInput}
                />
              </Grid.Column>
              <Grid.Column width={2}>
                <strong>Activo</strong>
                <Field name="active" component={SliderInput} type="checkbox" />
              </Grid.Column>
              <Grid.Column width={2}>
                <strong>Bloqueado</strong>
                <Field name="locked" component={SliderInput} type="checkbox" />
              </Grid.Column>
              <Grid.Column width={6}>
                <strong>Rol</strong>
                <Field
                  name="roleId"
                  placeholder="Rol"
                  options={roleOptions ?? []}
                  component={SelectInput}
                />
              </Grid.Column>
              <Grid.Column width={6}>
                <strong>Jefe directo</strong>
                <Field
                  name="managerId"
                  placeholder="Jefe directo"
                  options={lineManagersOptions ?? []}
                  component={SelectInput}
                  clearable={true}
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
        )}
      />
    </Segment>
  );
};

export default observer(UserForm);
