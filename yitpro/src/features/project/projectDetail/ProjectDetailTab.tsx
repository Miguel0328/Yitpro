import React from "react";
import { Tab } from "semantic-ui-react";
import ProjectForm from "../ProjectForm";
import ProjectTeam from "./ProjectTeam/ProjectTeam";

const panes = [
  {
    menuItem: { key: "indicators", icon: "line graph", content: "Indicadores" },
    render: () => <Tab.Pane>Tab 1 Content</Tab.Pane>,
  },
  {
    menuItem: { key: "team", icon: "users", content: "Equipo" },
    render: () => <ProjectTeam />,
  },
  {
    menuItem: {
      key: "Activities",
      icon: "pencil",
      content: "Actividades",
    },
    render: () => <Tab.Pane>Tab 1 Content</Tab.Pane>,
  },
  {
    menuItem: { key: "save", icon: "save", content: "Editar" },
    render: () => <ProjectForm />,
  },
];

const ProjectDetailTab = () => {
  return (
    <Tab
      grid={{ paneWidth: 13, tabWidth: 3 }}
      menu={{
        fluid: true,
        vertical: true,
        tabular: true,
      }}
      panes={panes}
    />
  );
};

export default ProjectDetailTab;
