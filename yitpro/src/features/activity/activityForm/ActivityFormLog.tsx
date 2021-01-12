import { formatDistance } from "date-fns";
import { es } from "date-fns/locale";
import { observer } from "mobx-react-lite";
import React from "react";
import { Comment, Grid } from "semantic-ui-react";
import { IActivityComment } from "../../../app/models/activity";

interface IProps {
  comments: IActivityComment[];
}

const ActivityFormLog: React.FC<IProps> = ({ comments }) => {
  return (
    <Comment.Group size="tiny">
      {comments &&
        comments.map((c) => (
          <Comment key={c.id}>
            <Comment.Avatar
              src={c.user.photo ? c.user.photo : "/assets/avatar.png"}
            />
            <Comment.Content>
              <Comment.Author>{c.user.name}</Comment.Author>
              <Comment.Metadata style={{ marginLeft: 0 }}>
                <span>
                  {formatDistance(c.date, new Date(), { locale: es })}
                </span>
              </Comment.Metadata>
              <Comment.Text>{c.comment}</Comment.Text>
            </Comment.Content>
          </Comment>
        ))}
    </Comment.Group>
  );
};

export default observer(ActivityFormLog);
