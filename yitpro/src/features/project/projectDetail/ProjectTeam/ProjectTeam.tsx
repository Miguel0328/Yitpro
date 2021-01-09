import React from "react";
import {
  Segment,
} from "semantic-ui-react";
import ProjectTeamHeader from "./ProjectTeamHeader";
import ProjectTeamTable from "./ProjectTeamTable";

const ProjectTeam = () => {
  return (
    <Segment className="principal-segment project-detail-segment">
      <ProjectTeamHeader />
      <ProjectTeamTable />
    </Segment>
  );
};

export default ProjectTeam;
