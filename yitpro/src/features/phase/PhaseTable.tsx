import { Switch } from "@material-ui/core";
import { observer } from "mobx-react-lite";
import React, { useContext, useEffect } from "react";
import { Segment, Icon, Button } from "semantic-ui-react";
import TableComponent from "../../app/common/table/TableComponent";
import { IPhase } from "../../app/models/phase";
import { IColumn } from "../../app/models/table";
import { RootStoreContext } from "../../app/stores/root";
// import PhaseFilter from "./PhaseFilter";
// import PhaseForm from "./PhaseForm";

const PhaseTable = () => {
  const rootStore = useContext(RootStoreContext);
  const {
    loadingPhases,
    filteredPhases: phases,
    phase: current,
    get,
    getById,
    putPSP,
    getClasifications,
    clearPhases,
    setPhase,
    // putEnabled,
    // download,
    filterPhases,
  } = rootStore.phaseStore;
  //   const { openModal } = rootStore.modalStore;

  useEffect(() => {
    get();

    return () => {
      clearPhases();
    };
  }, [get, clearPhases]);

  const columns: IColumn[] = [
    {
      id: "name",
      label: "Nombre",
      align: "left",
      width: "84%",
      style: (phase: IPhase) => {
        return phase.id === current?.id ? { backgroundColor: "lightgray" } : {};
      },
      render: (phase: IPhase) => (
        <Button
          as="a"
          className="a-button"
          onClick={() => {
            setPhase(phase.id);
            getClasifications();
          }}
        >
          {phase.name}
        </Button>
      ),
    },
    {
      id: "psp",
      label: "PSP",
      align: "center",
      width: "16%",
      orderable: false,
      style: (phase: IPhase) => {
        return phase.id === current?.id ? { backgroundColor: "lightgray" } : {};
      },
      render: (phase: IPhase) =>
        phase.active && (
          <div className="table-actions">
            {phase.psp && (
              <Icon
                name="flag checkered"
                className="icon-table"
                onClick={() => {
                  alert("Agregar las fases del tracking");
                }}
              />
            )}
            <Switch
              size="small"
              color="primary"
              checked={phase.psp}
              onChange={() => {
                phase.psp = !phase.psp;
                putPSP(phase)
                  .catch(() => (phase.active = !phase.active))
                  .then(() => {
                    getById(phase.id);
                  });
              }}
            />
          </div>
        ),
    },
  ];

  return (
    <Segment loading={loadingPhases} className="segment-table">
      <TableComponent
        // filterComponent={<PhaseFilter />}
        orderColumn="name"
        title="Fases"
        columns={columns}
        data={phases}
        filterAction={filterPhases}
        // downloadAction={download}
        paginated={false}
        onlySearchAction={true}
      />
    </Segment>
  );
};

export default observer(PhaseTable);
