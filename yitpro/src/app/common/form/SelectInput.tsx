import React from "react";
import { FieldRenderProps } from "react-final-form";
import { Dropdown, Form, FormFieldProps, Label } from "semantic-ui-react";

interface IProps
  extends FieldRenderProps<number, HTMLInputElement>,
    FormFieldProps {}

const SelectInput: React.FC<IProps> = ({
  input,
  width,
  options,
  placeholder,
  loading,
  disabled,
  clearable = false,
  meta: { touched, error },
}) => {
  return (
    <Form.Field error={touched && !!error} width={width}>
      <Dropdown
        selection
        search
        disabled={disabled}
        loading={loading}
        clearable={clearable}
        value={input.value}
        onChange={(e, data) => input.onChange(data.value)}
        placeholder={placeholder}
        options={options}
      />
      {touched && error && (
        <Label basic color="red">
          {error}
        </Label>
      )}
    </Form.Field>
  );
};

export default SelectInput;
