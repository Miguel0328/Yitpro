import { profile } from "console";
import React from "react";
import { Grid, Segment, Statistic } from "semantic-ui-react";
import RadialChart from "./chart";
import NivoBullet from "./NivoBullet";
import MyResponsivePie from "./NivoPieChart";
import MyResponsiveSunburst from "./NivoSunBurst";

const Home = () => {
  const dataPie = [
    {
      id: "javascript",
      label: "javascript",
      value: 502,
      color: "hsl(52, 70%, 50%)",
    },
    {
      id: "ruby",
      label: "ruby",
      value: 415,
      color: "hsl(138, 70%, 50%)",
    },
    {
      id: "stylus",
      label: "stylus",
      value: 362,
      color: "hsl(126, 70%, 50%)",
    },
    {
      id: "erlang",
      label: "erlang",
      value: 17,
      color: "hsl(150, 70%, 50%)",
    },
    {
      id: "hack",
      label: "hack",
      value: 295,
      color: "hsl(88, 70%, 50%)",
    },
  ];

  const dataSunBurst = {
    name: "nivo",
    color: "hsl(34, 70%, 50%)",
    children: [
      {
        name: "Total",
        color: "hsl(34, 20%, 50%)",
        children: [
          {
            name: "Avance",
            color: "hsl(34, 40%, 50%)",
            loc: 56,
          },
          {
            name: "Pendiente",
            color: "hsl(34, 60%, 50%)",
            loc: 27,
          },
        ],
      },
    ],
  };

  const dataBullet = [
    {
      id: "temp.",
      ranges: [84, 7, 95, 0, 120],
      measures: [105],
      markers: [73],
    },
  ];

  return (
    <Grid>
      <Grid.Column style={{ height: "300px" }} width="6">
        <Statistic.Group widths={2}>
          <Statistic label="Followers" value="16" />
          <Statistic label="Following" value="120" />
        </Statistic.Group>
      </Grid.Column>
      <Grid.Column style={{ height: "300px" }} width="3">
        <NivoBullet data={dataBullet} />
      </Grid.Column>
      <Grid.Column style={{ height: "300px" }} width="4">
        <MyResponsivePie data={dataPie} />
      </Grid.Column>
    </Grid>
  );
};

export default Home;
