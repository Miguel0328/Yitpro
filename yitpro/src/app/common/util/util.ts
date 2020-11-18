import { AxiosResponse } from "axios";
import { Messages } from "../../models/messages";

export const getErrors = (error: AxiosResponse) =>
  `${error.statusText}\n
${
  error.status === 403
    ? Messages.forbidden
    : error.data && error.status === 500
    ? error.data.errors
    : Object.keys(error.data.errors).length > 0 &&
      Object.values(error.data.errors)
        .flat()
        .map((err: any) => err)
}`;
