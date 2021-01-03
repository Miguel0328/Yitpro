import { observer } from "mobx-react-lite";
import React, { useContext, useEffect } from "react";
import { Icon, Image, Segment } from "semantic-ui-react";
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
    getTeam,
    clearTeam,
    setSelected,
    deleteTeam,
    downloadTeam,
  } = rootStore.projectTeamStore;
  const { openConfirmationModal } = rootStore.modalStore;

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
              openConfirmationModal(team.user, "deletion", () => {
                setSelected([team.id]);
                deleteTeam();
              });
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
        openConfirmationModal(
          selected.length.toString(),
          "deletion",
          deleteTeam,
          true
        );
      }}
    />
  );

  return (
    <Segment loading={loading} className="segment-table">
      <TableComponent
        orderColumn="user"
        columns={columns}
        data={team}
        selectable={true}
        selectionActions={selectionActions}
        selected={selected}
        setSelected={setSelected}
        filterAction={filterByText}
        downloadAction={downloadTeam}
      />
    </Segment>
  );
};

export default observer(ProjectTeamTable);
