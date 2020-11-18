import axios, { AxiosResponse } from "axios";
import { toast } from "react-toastify";
import { history } from "../..";

axios.defaults.baseURL = "http://localhost:5000/api/";

axios.interceptors.request.use(
  (config) => {
    const token = window.localStorage.getItem("jwt");
    if (token) config.headers.Authorization = `Bearer ${token}`;
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

axios.interceptors.response.use(undefined, (error) => {
  if (error.message === "Network Error" && !error.response) {
    toast.error("Error de red - Asegurate de tener conexión a Internet");
  }

  const { status, headers } = error.response;

  if (
    status === 401 &&
    headers["www-authenticate"]?.includes("The token expired")
  ) {
    window.localStorage.removeItem("jwt");
    history.push("/login");
    toast.info("La sesión expiró, por favor inicia sesión nuevamente");
  }

  if (status === 500) {
    toast.error(
      "Error en el servidor, contacta a tu administrador de sistemas"
    );
  }

  if (!headers["www-authenticate"]?.includes("The token expired"))
    throw error.response;
});

const responseBody = (response: AxiosResponse) => response?.data;

const sleep = (ms: number) => (response: AxiosResponse) =>
  new Promise<AxiosResponse>((resolve) =>
    setTimeout(() => resolve(response), ms)
  );

const requests = {
  get: (url: string, id?: number) =>
    axios
      .get(!id ? url : `${url}/${id}`)
      .then(sleep(500))
      .then(responseBody),
  post: (url: string, body: {}) =>
    axios.post(url, body).then(sleep(500)).then(responseBody),
  put: (url: string, body: {}) =>
    axios.put(url, body).then(sleep(500)).then(responseBody),
  delete: (url: string) =>
    axios.delete(url).then(sleep(500)).then(responseBody),
};

export default requests;
