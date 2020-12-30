import React from "react";
import { Segment, List, Divider, Checkbox } from "semantic-ui-react";
import { IShow } from "../../models/table";

interface IProps {
  displayedColumns: IShow[];
  visibleColumns: IShow[];
  handleDisplayColumn: (e: React.MouseEvent<HTMLInputElement>) => void;
}

const TableColumns: React.FC<IProps> = ({
  displayedColumns,
  visibleColumns,
  handleDisplayColumn,
}) => {
  return (
    <Segment className="columns-segment" inverted clearing>
      <List verticalAlign="middle">
        <List.Item key="columns">
          <List.Content>
            <List.Header style={{ color: "white" }}>Columnas</List.Header>
          </List.Content>
        </List.Item>
        <Divider style={{ marginTop: 3, marginBottom: 3 }} />
        {displayedColumns.map((x) => (
          <List.Item key={x.name}>
            <List.Content>
              <List.Header>
                <Checkbox
                  defaultChecked={
                    visibleColumns.find((y) => y.name === x.name)!.visible
                  }
                  onClick={handleDisplayColumn}
                  label={{
                    children: x.name[0].toUpperCase() + x.name.slice(1),
                  }}
                />
              </List.Header>
            </List.Content>
          </List.Item>
        ))}
      </List>
    </Segment>
  );
};

export default TableColumns;
