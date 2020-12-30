import React from "react";
import { Card, Container, Feed, Icon } from "semantic-ui-react";

const CalendarInfo = () => {
  return (
    <Container style={{ maxHeight: 500, overflowY: "auto" }}>
      <Card style={{ width: "unset" }}>
        <Card.Content>
          <Card.Header>Eventos</Card.Header>
        </Card.Content>
        <Card.Content>
          <Feed>
            <Feed.Event>
              <Feed.Label>
                <Icon circular fitted={true} className="coffee" />
              </Feed.Label>
              <Feed.Content>
                <Feed.Date content="1 day ago" />
                <Feed.Summary>
                  You added Jenny Hess to your coworker group.
                </Feed.Summary>
              </Feed.Content>
            </Feed.Event>
            <Feed.Event>
              <Feed.Label>
                <Icon circular fitted={true} className="birthday" />
              </Feed.Label>
              <Feed.Content>
                <Feed.Date content="3 days ago" />
                <Feed.Summary>
                  You added Molly Malone as a friend.
                </Feed.Summary>
              </Feed.Content>
            </Feed.Event>
            <Feed.Event>
              <Feed.Label>
                <Icon circular fitted={true} className="bolt" />
              </Feed.Label>
              <Feed.Content>
                <Feed.Date content="4 days ago" />
                <Feed.Summary>
                  You added Elliot Baker to your musicians group.
                </Feed.Summary>
              </Feed.Content>
            </Feed.Event>
          </Feed>
        </Card.Content>
      </Card>
      <Card style={{ width: "unset" }}>
        <Card.Content>
          <Card.Header>Cumpleaños</Card.Header>
        </Card.Content>
        <Card.Content>
          <Feed>
            <Feed.Event>
              <Feed.Label>
                <Icon circular fitted={true} className="coffee" />
              </Feed.Label>
              <Feed.Content>
                <Feed.Date content="1 day ago" />
                <Feed.Summary>
                  You added Jenny Hess to your coworker group.
                </Feed.Summary>
              </Feed.Content>
            </Feed.Event>
            <Feed.Event>
              <Feed.Label>
                <Icon circular fitted={true} className="birthday" />
              </Feed.Label>
              <Feed.Content>
                <Feed.Date content="3 days ago" />
                <Feed.Summary>
                  You added Molly Malone as a friend.
                </Feed.Summary>
              </Feed.Content>
            </Feed.Event>
          </Feed>
        </Card.Content>
      </Card>
    </Container>
  );
};

export default CalendarInfo;
