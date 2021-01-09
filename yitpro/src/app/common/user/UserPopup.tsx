import React from "react";
import { Image, Item, Label, Popup } from "semantic-ui-react";
import { IUser } from "../../models/user";

interface IProps {
  user: IUser;
}

const UserPopup: React.FC<IProps> = ({ user }) => {
  return (
    <Popup
      basic
      hoverable
      trigger={
        <Image
          src={user.photo ? user.photo : "/assets/avatar.png"}
          avatar
          size="mini"
        />
      }
    >
      <Item.Group style={{ width: "max-content" }}>
        <Item>
          <Item.Image
            size="tiny"
            src={user.photo ? user.photo : "/assets/avatar.png"}
          />
          <Item.Content>
            <Item.Header as="a">{user.name}</Item.Header>
            <Item.Meta>
              <span className="cinema">{user.role}</span>
            </Item.Meta>
            <Item.Extra>
              <Label icon="paperclip" content="0 proyectos" />
              <Label icon="trophy" content="0 actividades" />
            </Item.Extra>
          </Item.Content>
        </Item>
      </Item.Group>
    </Popup>
  );
};

export default UserPopup;
