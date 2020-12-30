import React, { useContext } from 'react'
import { Button, Header, Segment } from 'semantic-ui-react';
import { RootStoreContext } from '../../app/stores/root';
import ClientForm from './ClientForm';

const ClientHeader = () => {
  const rootStore = useContext(RootStoreContext);
  const { setClient } = rootStore.clientStore;
  const { openModal } = rootStore.modalStore;

    return (
        <Segment clearing className="segment-header">
          <Header
            as="h2"
            style={{ margin: 0 }}
            icon="handshake"
            className="icon-header"
            content="Clientes"
            floated="left"
          />
          <Button
            color="vk"
            content="Nuevo"
            floated="right"
            onClick={() => {
              setClient(0);
              openModal(<ClientForm />, "tiny", "Nuevo cliente");
            }}
          />
        </Segment>
      );
}

export default ClientHeader
