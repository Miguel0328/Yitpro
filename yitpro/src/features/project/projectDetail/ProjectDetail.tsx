import React, { useContext, useEffect } from "react";
import { Divider, Segment } from "semantic-ui-react";
import ProjectDetailTab from "../projectDetail/ProjectDetailTab";
import { RouteComponentProps } from "react-router-dom";
import ProjectDetailHeader from "./ProjectDetailHeader";
import { RootStoreContext } from "../../../app/stores/root";

interface ProjectParams {
  code: string;
}

const ProjectDetail: React.FC<RouteComponentProps<ProjectParams>> = ({
  match,
}) => {
  const rootStore = useContext(RootStoreContext);
  const {
    index,
    setProjectCode,
    getId,
    getDetail,
    clearProjectName,
  } = rootStore.projectStore;
  const { loadingIndex } = rootStore.commonStore;

  setProjectCode(match.params.code);

  useEffect(() => {
    index()
      .then(getId)
      .then(getDetail)
      .catch((error) => console.log(error));

    return () => {
      clearProjectName();
    };
  }, [index, getId, getDetail, clearProjectName]);

  if (loadingIndex) return null;

  return (
    <Segment className="principal-segment">
      <ProjectDetailHeader />
      <Divider section className="principal-divider" />
      <ProjectDetailTab />
    </Segment>
  );
};

export default ProjectDetail;
