import React from "react";
import {
  Segment,
} from "semantic-ui-react";
import ProjectDetailTeamHeader from "./ProjectTeamHeader";
import ProjectTeamTable from "./ProjectTeamTable";

const ProjectTeam = () => {
  return (
    <Segment className="project-detail-segment">
      <ProjectDetailTeamHeader />
      <ProjectTeamTable />
    </Segment>
  );
};

export default ProjectTeam;
