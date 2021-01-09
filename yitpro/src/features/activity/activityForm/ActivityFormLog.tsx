import React from "react";
import { Comment, Grid } from "semantic-ui-react";

const ActivityFormLog = () => {
  return (
    <Comment.Group size="tiny">
      <Comment>
        <Comment.Avatar as="a" src="/assets/avatar.png" />
        <Comment.Content>
          <Comment.Author as="a">Matt</Comment.Author>
          <Comment.Metadata>
            <span>Today at 5:42PM</span>
          </Comment.Metadata>
          <Comment.Text>How artistic!</Comment.Text>
        </Comment.Content>
      </Comment>
      <Comment>
        <Comment.Avatar as="a" src="/assets/avatar.png" />
        <Comment.Content>
          <Comment.Author as="a">Joe Henderson</Comment.Author>
          <Comment.Metadata>
            <span>5 days ago</span>
          </Comment.Metadata>
          <Comment.Text>Dude, this is awesome. Thanks so much</Comment.Text>
        </Comment.Content>
      </Comment>
      <Comment>
        <Comment.Avatar as="a" src="/assets/avatar.png" />
        <Comment.Content>
          <Comment.Author as="a">Joe Henderson</Comment.Author>
          <Comment.Metadata>
            <span>5 days ago</span>
          </Comment.Metadata>
          <Comment.Text>
            Dude, this is awesome. You're very welcome
          </Comment.Text>
        </Comment.Content>
      </Comment>
      <Comment>
        <Comment.Avatar as="a" src="/assets/avatar.png" />
        <Comment.Content>
          <Comment.Author as="a">Joe Henderson</Comment.Author>
          <Comment.Metadata>
            <span>5 days ago</span>
          </Comment.Metadata>
          <Comment.Text>Dude, this is awesome. Thanks so much</Comment.Text>
        </Comment.Content>
      </Comment>
    </Comment.Group>
  );
};

export default ActivityFormLog;