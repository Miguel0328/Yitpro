import React from "react";
import { FieldRenderProps } from "react-final-form";
import { Form, FormFieldProps, Label } from "semantic-ui-react";
import "react-semantic-ui-datepickers/dist/react-semantic-ui-datepickers.css";
import SemanticDatepicker from "react-semantic-ui-datepickers";

interface IProps
  extends FieldRenderProps<Date[], HTMLInputElement>,
    FormFieldProps {}

const DateRangeInput: React.FC<IProps> = ({
  input,
  width,
  type,
  clearable,
  placeholder,
  dateFormat = "DD/MM/YYYY",
  meta: { touched, error },
}) => {
  return (
    <Form.Field error={touched && !!error} type={type} width={width}>
      <div className="daterange-container">
        <SemanticDatepicker
          locale="es-ES"
          type="range"
          datePickerOnly={true}
          format={dateFormat}
          pointing="top left"
          showOutsideDays={true}
          clearable={clearable}
          showToday={false}
          placeholder={placeholder}
          value={input.value}
          onChange={(c: any, e: any) => {
            input.onChange(e.value);
          }}
        />
      </div>
      {touched && error && (
        <Label basic color="red">
          {error}
        </Label>
      )}
    </Form.Field>
  );
};

export default DateRangeInput;
