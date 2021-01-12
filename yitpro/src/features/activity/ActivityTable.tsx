import { Avatar, Switch } from "@material-ui/core";
import { observer } from "mobx-react-lite";
import React, { Fragment, useContext, useEffect } from "react";
import {
  Segment,
  Icon,
  Image,
  Progress,
  Popup,
  Card,
  Grid,
  Button,
  Item,
  Label,
  Dropdown,
} from "semantic-ui-react";
import TableComponent from "../../app/common/table/TableComponent";
import { IColumn } from "../../app/models/table";
import { IActivity } from "../../app/models/activity";
import { RootStoreContext } from "../../app/stores/root";
// import ActivityFilter from "./ActivityFilter";
import ActivityForm from "./activityForm/ActivityForm";
// import ActivityFilter from "./ActivityFilter";
import { Link } from "react-router-dom";
import UserPopup from "../../app/common/user/UserPopup";
import { Constants } from "../../app/models/constants";

const ActivityTable = () => {
  const rootStore = useContext(RootStoreContext);
  const {
    loading,
    filtered: activities,
    get,
    clearActivities,
    setActivityId,
    // putEnabled,
    // download,
    // filterByText,
  } = rootStore.activityStore;
  const { openModal } = rootStore.modalStore;

  useEffect(() => {
    get();

    return () => {
      clearActivities();
    };
  }, [get, clearActivities]);

  const columns: IColumn[] = [
    {
      id: "id",
      label: "Actividad",
      align: "left",
      width: "15%",
      render: (activity: IActivity) => {
        let color: "yellow" | "red" | "" = "";
        if (activity.critical && activity.urgent) color = "red";
        else if (activity.critical || activity.urgent) color = "yellow";
        return (
          <Fragment>
            {color && <Icon name="exclamation triangle" color={color} />}{" "}
            <Button
              className="a-button"
              onClick={() => {
                setActivityId(activity.id);
                openModal(<ActivityForm />, "large", activity.activity);
              }}
            >
              {activity.activity}
            </Button>
          </Fragment>
        );
      },
    },
    {
      id: "status",
      label: "Estatus",
      align: "left",
      width: "8%",
      style: (activity: IActivity) => {
        let background = "";
        switch (activity.status) {
          case Constants.activity.status.open:
            background = "#3fbae4";
            break;
          case Constants.activity.status.progress:
            background = "#fb7bfb";
            break;
          case Constants.activity.status.revision:
            background = "#ff9900";
            break;
          case Constants.activity.status.verified:
            background = "#2353b3";
            break;
          case Constants.activity.status.released:
            background = "#08C127";
            break;
          case Constants.activity.status.rejected:
            background = "#ff3333";
            break;
          case Constants.activity.status.cancelled:
            background = "#707070";
            break;
        }
        return {
          color: "white",
          backgroundColor: background,
          backgroundClip: "content-box",
          padding: "8px 0",
          textAlign: "center",
        };
      },
    },
    {
      id: "description",
      label: "Descripción",
      align: "left",
      width: "25%",
      contain: true,
    },
    {
      id: "phase",
      label: "Fase",
      align: "left",
      width: "12%",
    },
    {
      id: "assigned.name",
      label: "Asignado",
      align: "center",
      width: "5%",
      render: (activity: IActivity) => <UserPopup user={activity.assigned} />,
    },
    {
      id: "responsible.name",
      label: "Responsable",
      align: "center",
      width: "5%",
      render: (activity: IActivity) => (
        <UserPopup user={activity.responsible} />
      ),
    },
    {
      id: "time",
      label: "Tiempo",
      align: "left",
      width: "22%",
      render: (activity: IActivity) => {
        const percent = Math.round(
          (activity.finalTime / activity.assignedTime) * 100
        );
        const color =
          percent <= 100 ? "green" : percent <= 105 ? "yellow" : "red";
        return <Progress percent={percent} color={color} />;
      },
    },
    {
      id: "actions",
      label: "",
      align: "center",
      width: "3%",
      orderable: false,
      render: (activity: IActivity) => (
        <div className="table-actions">
          <Dropdown icon="ellipsis vertical" pointing="right">
            <Dropdown.Menu>
              <Dropdown.Item>
                <Icon name="clock outline" color="blue" />
                Agregar tiempo
              </Dropdown.Item>
              <Dropdown.Item>
                <Icon name="check circle outline" color="green" />
                Solicitar revisión
              </Dropdown.Item>
              <Dropdown.Divider />
              <Dropdown.Item>
                <Icon name="times circle outline" color="red" />
                Cancelar
              </Dropdown.Item>
            </Dropdown.Menu>
          </Dropdown>
        </div>
      ),
    },
  ];

  // const rowStyle = (activity: IActivity) => {
  //   return new Date(activity.endDate) <
  //     (activity.finalDate ? new Date(activity.finalDate) : new Date())
  //     ? { backgroundColor: "#ffbbbb" }
  //     : {};
  // };

  return (
    <Segment loading={loading} className="segment-table">
      <TableComponent
        orderColumn="id"
        orderDirection="desc"
        columns={columns}
        data={activities}
        // filterAction={filterByText}
        // downloadAction={download}
        // filterComponent={<ActivityFilter />}
        // rowStyle={rowStyle}
      />
    </Segment>
  );
};

export default observer(ActivityTable);
