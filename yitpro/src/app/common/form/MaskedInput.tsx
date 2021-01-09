import React from "react";
import { FieldRenderProps } from "react-final-form";
import { Form, FormFieldProps, Label } from "semantic-ui-react";
import InputMask from "react-input-mask";

interface IProps
  extends FieldRenderProps<string, HTMLInputElement>,
    FormFieldProps {}

const MaskInput: React.FC<IProps> = ({
  input,
  width,
  type,
  placeholder,
  mask,
  meta: { touched, error },
}) => {
  return (
    <Form.Field error={touched && !!error} type={type} width={width}>
      <InputMask
        placeholder={placeholder}
        disabled={false}
        mask={mask}
        {...input}
        alwaysShowMask={false}
        children={<Form.Input />}
      ></InputMask>
      {touched && error && (
        <Label basic color="red">
          {error}
        </Label>
      )}
    </Form.Field>
  );
};

export default MaskInput;
