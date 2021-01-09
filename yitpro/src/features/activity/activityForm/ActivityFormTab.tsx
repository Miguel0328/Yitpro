import React, { Fragment, useState } from "react";
import { Comment, Header, Tab } from "semantic-ui-react";
import ActivityForm from "./ActivityFormDetail";
import { Editor } from "@tinymce/tinymce-react";
import { useDropzone } from "react-dropzone";
import { Parser } from "html-to-react";

const ActivityFormTab = () => {
  const [cont, setCont] = useState("");
  const handleEditorChange = (content: any, editor: any) => {
    setCont(content);
  };

  const { acceptedFiles, getRootProps, getInputProps } = useDropzone();

  const files = acceptedFiles.map((file: any) => (
    <li key={file.path}>
      {file.path} - {file.size} bytes
    </li>
  ));

  const parser = new Parser();

  const panes = [
    {
      menuItem: { key: "comments", icon: "comments", title: "Comentarios" },
      render: () => (
        <Fragment>
          {/* <Editor
            apiKey="51zzpzaz8nh1drv0voggbx8zliv7sfo5r4g4l5godden377i"
            onEditorChange={handleEditorChange}
            init={{
              height: 150,
              menubar: false,
              contextmenu: false,
              plugins: [
                "advlist autolink lists link image charmap print preview anchor",
                "searchreplace visualblocks code fullscreen",
                "insertdatetime media table paste code help wordcount",
              ],
              fontsize_formats: "12pt",
              toolbar: `bold italic backcolor bullist`,
              content_style:
                "* { padding: 0; margin: 0 } body {font-size: 10pt; padding: 5; } ul { padding-left: 5px }",
            }}
          /> */}
          <Comment.Group>
            <Header as="h4" dividing>
              Comentarios
            </Header>
            <div className="activity comment-form">
              <Comment>
                <Comment.Avatar as="a" src="/assets/avatar.png" />
                <Comment.Content>
                  <Comment.Author as="a">Matt</Comment.Author>
                  <Comment.Metadata>
                    <span>Today at 5:42PM</span>
                  </Comment.Metadata>
                  <Comment.Text> {parser.parse(cont)}</Comment.Text>
                  <Comment.Actions>
                    <a>Reply</a>
                  </Comment.Actions>
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
                    Dude, this is awesome. Thanks so much
                  </Comment.Text>
                  <Comment.Actions>
                    <a>Reply</a>
                  </Comment.Actions>
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
                    Dude, this is awesome. Thanks so much
                  </Comment.Text>
                  <Comment.Actions>
                    <a>Reply</a>
                  </Comment.Actions>
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
                    Dude, this is awesome. Thanks so much
                  </Comment.Text>
                  <Comment.Actions>
                    <a>Reply</a>
                  </Comment.Actions>
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
                    Dude, this is awesome. Thanks so much
                  </Comment.Text>
                  <Comment.Actions>
                    <a>Reply</a>
                  </Comment.Actions>
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
                    Dude, this is awesome. Thanks so much
                  </Comment.Text>
                  <Comment.Actions>
                    <a>Reply</a>
                  </Comment.Actions>
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
                    Dude, this is awesome. Thanks so much
                  </Comment.Text>
                  <Comment.Actions>
                    <a>Reply</a>
                  </Comment.Actions>
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
                    Dude, this is awesome. Thanks so much
                  </Comment.Text>
                  <Comment.Actions>
                    <a>Reply</a>
                  </Comment.Actions>
                </Comment.Content>
              </Comment>
            </div>
          </Comment.Group>
        </Fragment>
      ),
    },
    {
      menuItem: { key: "clock", icon: "clock" },
      render: () => <h1>Aqui van los tiempos Aqui van los comentarios </h1>,
    },
    {
      menuItem: { key: "file-alternate", icon: "file alternate" },
      render: () => (
        <div {...getRootProps({ className: "dropzone" })}>
          <input {...getInputProps()} />
          <p>Drag 'n' drop some files here, or click to select files</p>
        </div>
      ),
    },
    // {
    //   menuItem: { key: "exchange", icon: "exchange" },
    //   render: () => (
    //     <h1>Aqui van las autorizaciones Aqui van los comentarios</h1>
    //   ),
    // },
    {
      menuItem: { key: "check circle", icon: "check circle" },
      render: () => (
        <h1>Aqui van las autorizaciones Aqui van los comentarios</h1>
      ),
    },
  ];

  return (
    <Tab
      className="activity-tab"
      menu={{ fluid: true, tabular: "right" }}
      grid={{ paneWidth: 14, tabWidth: 2 }}
      panes={panes}
    />
  );
};

export default ActivityFormTab;
