import { observer } from "mobx-react-lite";
import React, { useContext, useEffect } from "react";
import {
  Icon,
  Image,
  Segment,
} from "semantic-ui-react";
import TableComponent from "../../../../app/common/table/TableComponent";
import { IProjectTeam } from "../../../../app/models/project";
import { IColumn } from "../../../../app/models/table";
import { RootStoreContext } from "../../../../app/stores/root";

const ProjectTeamTable = () => {
  const rootStore = useContext(RootStoreContext);
  const {
    loading,
    filteredTeam: team,
    selected,
    filterByText,
    setProjectId,
    getTeam,
    clearTeam,
    setSelected,
  } = rootStore.projectTeamStore;
  const { projectId } = rootStore.projectStore;
  const { openDeletionModal } = rootStore.modalStore;

  setProjectId(projectId);

  useEffect(() => {
    getTeam();

    return () => {
      clearTeam();
    };
  }, [getTeam, clearTeam]);

  const columns: IColumn[] = [
    {
      id: "user",
      label: "Usuario",
      align: "left",
      width: "92%",
      render: (team: IProjectTeam) => (
        <div
          style={{
            display: "flex",
            flexDirection: "row",
            justifyContent: "flex-start",
            alignItems: "center",
          }}
        >
          <Image
            src={team.userPhoto ? team.userPhoto : "/assets/avatar.png"}
            avatar
          />
          <span>{team.user}</span>
        </div>
      ),
    },
    {
      id: "actions",
      label: "Acciones",
      align: "center",
      width: "8%",
      orderable: false,
      render: (team: IProjectTeam) => (
        <div className="table-actions">
          <Icon
            name="trash"
            className="icon-table"
            onClick={() => {
              openDeletionModal(team.user, () => alert("hola"));
            }}
          />
        </div>
      ),
    },
  ];

  const selectionActions = (
    <Icon
      className="icon-table-header"
      size="large"
      name="trash"
      onClick={() => {
        openDeletionModal(
          selected.length.toString(),
          () => alert("hola"),
          true
        );
      }}
    />
  );

  return (
    <Segment loading={loading} className="segment-table">
      <TableComponent
        orderColumn="code"
        columns={columns}
        data={team}
        selectable={true}
        selectionActions={selectionActions}
        selected={selected}
        setSelected={setSelected}
        filterAction={filterByText}
        // downloadAction={download}
        // filterComponent={<ProjectFilter />}
      />
    </Segment>
  );
};

export default observer(ProjectTeamTable);
