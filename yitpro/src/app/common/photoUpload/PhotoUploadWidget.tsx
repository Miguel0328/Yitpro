import { observer } from "mobx-react-lite";
import React, { Fragment, useContext, useEffect, useState } from "react";
import { Button, Grid } from "semantic-ui-react";
import { RootStoreContext } from "../../stores/root";
import PhotoWidgetCropper from "./PhotoWidgetCropper";
import PhotoWidgetDropzone from "./PhotoWidgetDropzone";

interface IProps {
  setPhoto: any;
  action?: any;
}

const PhotoUploadWidget: React.FC<IProps> = ({ setPhoto, action }) => {
  const rootStore = useContext(RootStoreContext);
  const { closeUpperModal } = rootStore.modalStore;

  const [files, setFiles] = useState<any[]>([]);
  const [image, setImage] = useState<Blob | null>(null);

  useEffect(() => {
    return () => {
      files.forEach((file) => URL.revokeObjectURL(file.preview));
    };
  });

  return (
    <Fragment>
      <Grid>
        <Grid.Column width={5}>
          <strong>Agregar foto</strong>
          <PhotoWidgetDropzone setFiles={setFiles} />
        </Grid.Column>
        <Grid.Column width={6}>
          <strong>Resize</strong>
          {files.length > 0 && (
            <PhotoWidgetCropper
              setImage={setImage}
              imagePreview={files[0].preview}
            />
          )}
        </Grid.Column>
        <Grid.Column width={5}>
          <strong>Preview</strong>
          {files.length > 0 && (
            <div className="preview-container">
              <div
                className="img-preview"
                style={{
                  minHeight: "150px",
                  overflow: "hidden",
                  border: "1px solid gray",
                  width: "100%",
                }}
              ></div>
              <Button.Group style={{ width: "100%" }} widths={2}>
                <Button
                  positive
                  icon="check"
                  onClick={() => {
                    setPhoto(image!);
                    closeUpperModal();
                    if (action) action();
                  }}
                />
                <Button icon="close" onClick={() => setFiles([])} />
              </Button.Group>
            </div>
          )}
        </Grid.Column>
      </Grid>
    </Fragment>
  );
};

export default observer(PhotoUploadWidget);
