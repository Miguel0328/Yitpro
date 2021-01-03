import { observer } from "mobx-react-lite";
import React, { useContext, useEffect } from "react";
import { Icon, Image, Segment } from "semantic-ui-react";
import TableComponent from "../../../../app/common/table/TableComponent";
import { IColumn } from "../../../../app/models/table";
import { IUser } from "../../../../app/models/user";
import { RootStoreContext } from "../../../../app/stores/root";

const ProjectTeamTable = () => {
  const rootStore = useContext(RootStoreContext);
  const {
    filteredRemaining: remaining,
    selectedRemaining: selected,
    submitting,
    postTeam,
    filterRemainingByText,
    getRemaining,
    clearRemaining,
    setSelectedRemaining: setSelected,
  } = rootStore.projectTeamStore;
  const { openConfirmationModal } = rootStore.modalStore;

  useEffect(() => {
    getRemaining();

    return () => {
      clearRemaining();
    };
  }, [getRemaining, clearRemaining]);

  const columns: IColumn[] = [
    {
      id: "name",
      label: "Usuario",
      align: "left",
      width: "92%",
      render: (user: IUser) => (
        <div
          style={{
            display: "flex",
            flexDirection: "row",
            justifyContent: "flex-start",
            alignItems: "center",
          }}
        >
          <Image src={user.photo ? user.photo : "/assets/avatar.png"} avatar />
          <span>{user.name}</span>
        </div>
      ),
    },
    {
      id: "actions",
      label: "Acciones",
      align: "center",
      width: "8%",
      orderable: false,
      render: (team: IUser) => (
        <div className="table-actions">
          <Icon
            name="user plus"
            className="icon-table"
            onClick={() => {
              openConfirmationModal(team.name, "confirmation", () => {
                setSelected([team.id]);
                postTeam();
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
      name="user plus"
      onClick={() => {
        openConfirmationModal(
          selected.length.toString(),
          "confirmation",
          postTeam,
          true
        );
      }}
    />
  );

  return (
    <Segment loading={submitting} className="segment-table">
      <TableComponent
        orderColumn="name"
        columns={columns}
        data={remaining}
        selectable={true}
        selectionActions={selectionActions}
        selected={selected}
        setSelected={setSelected}
        filterAction={filterRemainingByText}
      />
    </Segment>
  );
};

export default observer(ProjectTeamTable);
