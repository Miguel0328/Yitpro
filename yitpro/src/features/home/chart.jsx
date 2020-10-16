import React, { PureComponent } from 'react';
import { RadialBarChart, RadialBar, Legend } from 'recharts';


const data = [
  {
    name: "Objetivo",
    uv: 102,
    pv: 1800,
    fill: "#82ca9d",
  },
  {
    name: "Esperadas",
    uv: 54,
    pv: 3908,
    fill: "#a4de6c",
  },
  {
    name: "Actual",
    uv: 22.64,
    pv: 4800,
    fill: "red",
  },
];

const style = {
  top: 40,
  lineHeight: '30px',
  left:40,
  aling:"right"
};


export default class RadialChart extends PureComponent {

  render() {
    return (
      <RadialBarChart width={600} height={500} cx={350} cy={150} innerRadius={20} outerRadius={170} barSize={60} data={data}>
        <RadialBar minAngle={15} label={{ position: 'insideCenter', fill: '#fff' }} background clockWise dataKey="uv" />
        <Legend iconSize={20} width={120} height={140} layout="vertical" verticalAlign="middle" wrapperStyle={style} />
      </RadialBarChart>
    );
  }
}