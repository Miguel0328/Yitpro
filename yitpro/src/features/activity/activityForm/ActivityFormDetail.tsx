import React, { Fragment, useContext, useEffect } from "react";
import { Form as FinalForm, Field } from "react-final-form";
import {
  Button,
  Dropdown,
  Form,
  Grid,
  Icon,
  Label,
  Progress,
  Segment,
} from "semantic-ui-react";
import SelectInput from "../../../app/common/form/SelectInput";
import SliderInput from "../../../app/common/form/SliderInput";
import TextInput from "../../../app/common/form/TextInput";
import MaskInput from "../../../app/common/form/MaskedInput";
import DateRangeInput from "../../../app/common/form/DateRangeInput";
import TextAreaInput from "../../../app/common/form/TextAreaInput";
import DateInput from "../../../app/common/form/DateInput";
import { RootStoreContext } from "../../../app/stores/root";
import {
  ActivityFormValues,
  IActivityDetail,
} from "../../../app/models/activity";
import { observer } from "mobx-react-lite";
import { OnChange } from "react-final-form-listeners";
import ActivityFormLog from "./ActivityFormLog";
import Activity from "../Activity";

const ActivityFormDetail = () => {
  const rootStore = useContext(RootStoreContext);
  const {
    activityId,
    activity: currentActivity,
    submitting,
    getDetail,
    initForm,
    post,
    clearActivity,
  } = rootStore.activityStore;
  const {
    projectOptions,
    responsibleOptions,
    projectPhaseOptions,
    clasificationOptions,
    projectTeamOptions,
    loadingClasifications,
    loadingProjectTeam,
    getClasificationOptions,
    getProjectTeamOptions,
    clearClasificationOptions,
    clearProjectTeamOptions,
  } = rootStore.optionStore;

  useEffect(() => {
    initForm();
    if (activityId !== 0) {
      getDetail();
    }

    return () => {
      clearActivity();
    };
  }, [activityId, initForm, getDetail, clearActivity]);

  let activity = new ActivityFormValues(currentActivity);

  const handleChangePhase = (value: any) => {
    if (value > 0) getClasificationOptions(value);
    clearClasificationOptions();
  };

  const handleChangeProject = (value: any) => {
    if (value > 0) getProjectTeamOptions(value);
    else clearProjectTeamOptions();
  };

  const handleSubmit = async (activity: IActivityDetail) => {
    activity.projectId = activity.projectId === "" ? 0 : activity.projectId;
    activity.phaseId = activity.phaseId === "" ? 0 : activity.phaseId;
    activity.clasificationId =
      activity.clasificationId === "" ? 0 : activity.clasificationId;
    activity.assignedId = activity.assignedId === "" ? 0 : activity.assignedId;
    activity.responsibleId =
      activity.responsibleId === "" ? 0 : activity.responsibleId;
    activity.estimatedTime = activity.estimatedTime.replace(/_/g, "0");
    activity.assignedTime = activity.assignedTime.replace(/_/g, "0");

    post(activity);
  };

  return (
    <Segment className="form-container" basic loading={submitting}>
      <FinalForm
        initialValues={activity}
        keepDirtyOnReinitialize={true}
        onSubmit={handleSubmit}
        // validate={validate}
        mutators={{
          clear: ([name], state, { changeValue }) => {
            changeValue(state, name, () => "");
          },
        }}
        render={({ handleSubmit, invalid, pristine, values, form }) => {
          const final =
            parseInt(currentActivity?.finalTime.split(":")[0] ?? "0") * 3600 +
            parseInt(currentActivity?.finalTime.split(":")[1] ?? "0") * 60;
          const assigned =
            parseInt(currentActivity?.assignedTime.split(":")[0] ?? "0") *
              3600 +
            parseInt(currentActivity?.assignedTime.split(":")[1] ?? "0") * 60;
          const percent = Math.round((final / assigned) * 100);
          const color =
            percent <= 100 ? "green" : percent <= 105 ? "yellow" : "red";
          return (
            <Form onSubmit={handleSubmit} error>
              <Grid stackable className="no-padding">
                <Grid.Column width={!!activityId ? 4 : 12}>
                  <strong className="false-label">Estatus</strong>
                  <Label className="form-label" color="teal">
                    Abierto
                  </Label>
                </Grid.Column>
                {!!activityId && (
                  <Grid.Column width={8} verticalAlign="bottom">
                    <Grid>
                      <strong>{activity.finalTime} horas</strong>
                      <Grid.Column width={14} style={{ paddingRight: 10 }}>
                        {" "}
                        <Progress
                          className="form-progress"
                          percent={percent}
                          color={color}
                        />
                      </Grid.Column>
                      <Grid.Column width={2} className="form-clock">
                        <Icon.Group size="big" onClick={() => alert("Hola")}>
                          <Icon name="clock outline" />
                          <Icon corner="top right" name="add" />
                        </Icon.Group>
                      </Grid.Column>
                    </Grid>
                  </Grid.Column>
                )}
                <Grid.Column textAlign="right" width={4}>
                  <strong className="false-label">Acciones</strong>
                  {!!!activityId ? (
                    <Button color="vk">Guardar</Button>
                  ) : (
                    <Button.Group>
                      <Button color="vk">Guardar</Button>
                      <Dropdown
                        style={{
                          backgroundColor: "#4d7198",
                          color: "white",
                        }}
                        floating
                        className="button icon"
                      >
                        <Dropdown.Menu>
                          <Dropdown.Item
                            label={{
                              color: "red",
                              empty: true,
                              circular: true,
                            }}
                            text="Important"
                          />
                          <Dropdown.Item
                            label={{
                              color: "blue",
                              empty: true,
                              circular: true,
                            }}
                            text="Announcement"
                          />
                          <Dropdown.Item
                            label={{
                              color: "black",
                              empty: true,
                              circular: true,
                            }}
                            text="Discussion"
                          />
                        </Dropdown.Menu>
                      </Dropdown>
                    </Button.Group>
                  )}
                </Grid.Column>
                <Grid.Column width={4}>
                  <strong>Proyecto</strong>
                  <Field
                    name="projectId"
                    placeholder="Proyecto"
                    component={SelectInput}
                    options={projectOptions ?? []}
                    cleareable={true}
                  />
                  <OnChange name="projectId">
                    {(value) => {
                      handleChangeProject(value);
                      !pristine && form.mutators.clear("assignedId");
                    }}
                  </OnChange>
                </Grid.Column>
                <Grid.Column width={4}>
                  <strong>Fase</strong>
                  <Field
                    name="phaseId"
                    placeholder="Fase"
                    options={projectPhaseOptions ?? []}
                    component={SelectInput}
                  />
                  <OnChange name="phaseId">
                    {(value) => {
                      handleChangePhase(value);
                      !pristine && form.mutators.clear("clasificationId");
                    }}
                  </OnChange>
                </Grid.Column>
                <Grid.Column width={4}>
                  <strong>Clasificación</strong>
                  <Field
                    name="clasificationId"
                    placeholder="Clasificación"
                    options={clasificationOptions ?? []}
                    component={SelectInput}
                    disabled={!(values.phaseId > 0)}
                    loading={loadingClasifications}
                  />
                </Grid.Column>
                <Grid.Column
                  width={4}
                  style={{ borderLeft: "1px solid #e8e8e8" }}
                >
                  <strong>Asigando</strong>
                  <Field
                    name="assignedId"
                    placeholder="Asigando"
                    options={projectTeamOptions ?? []}
                    component={SelectInput}
                    disabled={!(values.projectId > 0)}
                    loading={loadingProjectTeam}
                  />
                </Grid.Column>
                <Grid.Column width={4}>
                  <strong>Horas estimadas</strong>
                  <Field
                    name="estimatedTime"
                    placeholder="00:00"
                    mask={[/[0-9]/, /[0-9]/, ":", /[0-5]/, /[0-9]/]}
                    component={MaskInput}
                  />
                </Grid.Column>
                <Grid.Column width={4}>
                  <strong>Horas asignadas</strong>
                  <Field
                    name="assignedTime"
                    placeholder="00:00"
                    mask={[/[0-9]/, /[0-9]/, ":", /[0-5]/, /[0-9]/]}
                    component={MaskInput}
                  />
                </Grid.Column>
                <Grid.Column width={4}>
                  <strong>Periodo</strong>
                  <Field
                    name="period"
                    placeholder="Periodo"
                    dateFormat="DD/MM/YY"
                    component={DateRangeInput}
                    clearable={false}
                  />
                </Grid.Column>
                <Grid.Column
                  width={4}
                  style={{ borderLeft: "1px solid #e8e8e8" }}
                >
                  <strong>Responsable</strong>
                  <Field
                    name="responsibleId"
                    placeholder="Responsable"
                    options={responsibleOptions ?? []}
                    component={SelectInput}
                  />
                </Grid.Column>
                <Grid.Column width={!!activityId ? 12 : 16}>
                  <strong>Requerimiento</strong>
                  <Field
                    name="requirement"
                    placeholder="Requerimiento"
                    component={TextInput}
                  />
                </Grid.Column>
                {!!activityId && (
                  <Grid.Column
                    width={4}
                    style={{ borderLeft: "1px solid #e8e8e8" }}
                  >
                    <strong>Fecha fin</strong>
                    <Field
                      name="finalDate"
                      placeholder="Fecha fin"
                      component={DateInput}
                      date={true}
                    />
                  </Grid.Column>
                )}
                <Grid.Row stretched>
                  <Grid.Column width={!!activityId ? 12 : 16}>
                    <Grid stackable>
                      <Grid.Column width={16}>
                        <strong>Descripción</strong>
                        <Field
                          name="description"
                          placeholder="Descripción"
                          rows={4}
                          component={TextAreaInput}
                        />
                      </Grid.Column>
                      <Grid.Column
                        style={{ width: !!activityId ? "12.5%" : "9.375%" }}
                      >
                        <strong>Critico</strong>
                        <Field
                          name="critical"
                          component={SliderInput}
                          type="checkbox"
                        />
                      </Grid.Column>
                      <Grid.Column
                        style={{ width: !!activityId ? "12.5%" : "9.375%" }}
                      >
                        <strong>Planeado</strong>
                        <Field
                          name="planned"
                          component={SliderInput}
                          type="checkbox"
                        />
                      </Grid.Column>
                      <Grid.Column
                        style={{ width: !!activityId ? "12.5%" : "9.375%" }}
                      >
                        <strong>Urgente</strong>
                        <Field
                          name="urgent"
                          component={SliderInput}
                          type="checkbox"
                        />
                      </Grid.Column>
                    </Grid>
                  </Grid.Column>
                  {!!activityId && (
                    <Grid.Column
                      style={{ borderLeft: "1px solid #e8e8e8" }}
                      width={4}
                      className="activity log-form"
                    >
                      <ActivityFormLog comments={activity.log} />
                    </Grid.Column>
                  )}
                </Grid.Row>
              </Grid>
            </Form>
          );
        }}
      />
    </Segment>
  );
};

export default observer(ActivityFormDetail);
