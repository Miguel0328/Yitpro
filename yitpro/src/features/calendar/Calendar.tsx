import React, { useEffect, useState } from "react";
import Calendar from "rc-year-calendar";
import "rc-year-calendar/locales/rc-year-calendar.es";
import { Segment, Divider, Grid } from "semantic-ui-react";
import CalendarHeader from "./CalendarHeader";
import CalendarInfo from "./CalendarInfo";

interface IDataSource {
  dataSource: ICalendar[];
}

interface ICalendar {
  id: number;
  description: string;
  type: number;
  startDate: Date;
  endDate: Date;
  color: string;
}

const CalendarComponent = () => {
  const [dates, setDataes] = useState<ICalendar[]>([]);

  useEffect(() => {
    console.log(dates);
  }, [dates]);

  return (
    <Segment loading={false} className="principal-segment">
      <CalendarHeader />
      <Divider section className="principal-divider" />
      <Grid>
        <Grid.Column className="calendar-year" width={11}>
          <Calendar
            language="es"
            onRangeSelected={(e: any) => {
              let newData = {
                id: dates.length + 1,
                description: "Evento " + (dates.length + 1).toString(),
                type: Math.floor(Math.random() * 3),
                startDate: e.startDate,
                endDate: e.endDate,
                color: "",
              };
              newData.color =
                newData.type === 0
                  ? "blue"
                  : newData.type === 1
                  ? "red"
                  : "green";
              setDataes((bef) => [...bef, newData]);
            }}
            dataSource={dates}
            // customDayRenderer={(element: any, date: any) => {
            //   if (date.getDay() === 6 || date.getDay() === 0) {
            //     element.style.backgroundColor = "#e62b5f59";
            //   }
            // }}
            roundRangeLimits={false}
            enableRangeSelection={true}
          />
        </Grid.Column>
        <Grid.Column width={5}>
          <CalendarInfo />
        </Grid.Column>
      </Grid>
    </Segment>
  );
};

export default CalendarComponent;
