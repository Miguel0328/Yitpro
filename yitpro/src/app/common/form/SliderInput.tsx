import React from "react";
import { FieldRenderProps } from "react-final-form";
import { Form, FormFieldProps } from "semantic-ui-react";

interface IProps
  extends FieldRenderProps<boolean, HTMLInputElement>,
    FormFieldProps {}

const SliderInput: React.FC<IProps> = ({
  width,
  type,
  input,
  meta: { touched, error },
  ...rest
}) => {
  return (
    <Form.Field width={width}>
      <div className="ui fitted toggle checkbox">
        <input type="checkbox" checked={input.checked} onChange={input.onChange} />
        <label></label>
      </div>
    </Form.Field>
  );
};

export default SliderInput;
