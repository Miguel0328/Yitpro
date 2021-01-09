import { Switch } from "@material-ui/core";
import { observer } from "mobx-react-lite";
import React, { useContext, useEffect } from "react";
import { Segment, Icon, Image, Button } from "semantic-ui-react";
import TableComponent from "../../app/common/table/TableComponent";
import { IColumn } from "../../app/models/table";
import { IProject, ProjectStatus } from "../../app/models/project";
import { RootStoreContext } from "../../app/stores/root";
// import ProjectFilter from "./ProjectFilter";
import ProjectForm from "./ProjectForm";
import ProjectFilter from "./ProjectFilter";
import { Link } from "react-router-dom";
import { history } from "../..";

const ProjectTable = () => {
  const rootStore = useContext(RootStoreContext);
  const {
    loading,
    filtered: projects,
    get,
    clearProjects,
    setProjectId,
    putEnabled,
    download,
    filterByText,
  } = rootStore.projectStore;
  const { openModal } = rootStore.modalStore;

  useEffect(() => {
    get();

    return () => {
      clearProjects();
    };
  }, [get, clearProjects]);

  const columns: IColumn[] = [
    {
      id: "code",
      label: "Clave",
      align: "left",
      width: "10%",
      render: (project: IProject) => (
        <Button
          className="a-button"
          onClick={()=>{
            history.push("project/detail/" + project.code)
          }}
        >
          {project.code}
        </Button>
      ),
    },
    {
      id: "name",
      label: "Nombre",
      align: "left",
      width: "20%",
    },
    {
      id: "client",
      label: "Cliente",
      align: "left",
      width: "15%",
    },
    {
      id: "leader",
      label: "Líder",
      align: "left",
      width: "22%",
      render: (project: IProject) => (
        <div
          style={{
            display: "flex",
            flexDirection: "row",
            justifyContent: "flex-start",
            alignItems: "center",
          }}
        >
          <Image
            src={
              project.leaderPhoto ? project.leaderPhoto : "/assets/avatar.png"
            }
            avatar
          />
          <span>{project.leader}</span>
        </div>
      ),
    },
    {
      id: "type",
      label: "Tipo",
      align: "left",
      width: "15%",
    },
    {
      id: "methodology",
      label: "Metodología",
      align: "left",
      width: "10%",
    },
    {
      id: "status",
      label: "Estatus",
      align: "left",
      width: "10%",
    },
    {
      id: "actions",
      label: "Acciones",
      align: "center",
      width: "8%",
      orderable: false,
      render: (project: IProject) => (
        <div className="table-actions">
          <Icon
            name="edit"
            className="icon-table"
            onClick={() => {
              console.log("Editando");
              setProjectId(project.id);
              openModal(<ProjectForm />, "small", "Editar: " + project.name);
            }}
          />
          <Switch
            size="small"
            color="primary"
            checked={project.active}
            onChange={() => {
              project.active = !project.active;
              putEnabled(project).catch(
                () => (project.active = !project.active)
              );
            }}
          />
        </div>
      ),
    },
  ];

  const rowStyle = (project: IProject) => {
    return project.status === ProjectStatus.canceled
      ? { backgroundColor: "#ffbbbb" }
      : {};
  };

  return (
    <Segment loading={loading} className="segment-table">
      <TableComponent
        orderColumn="code"
        columns={columns}
        data={projects}
        filterAction={filterByText}
        downloadAction={download}
        filterComponent={<ProjectFilter />}
        rowStyle={rowStyle}
      />
    </Segment>
  );
};

export default observer(ProjectTable);
