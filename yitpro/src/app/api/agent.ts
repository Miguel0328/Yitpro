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
    throw error.response;
  }

  const { status, headers } = error.response;

  if (
    status === 401 &&
    headers["www-authenticate"]?.includes("The token expired")
  ) {
    window.localStorage.removeItem("jwt");
    history.push("/login");
    toast.info("La sesión expiró, por favor inicia sesión nuevamente");
    return;
  }

  if (status === 500) {
    toast.error(
      "Error en el servidor, contacta a tu administrador de sistemas"
    );
    console.log(error.response);
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
  get: (url: string, id?: number | string) =>
    axios
      .get(!id ? url : `${url}/${id}`)
      .then(sleep(0))
      .then(responseBody),
  getBody: (url: string, body: {}) =>
    axios.post(url, body).then(sleep(0)).then(responseBody),
  post: (url: string, body: {}) =>
    axios.post(url, body).then(sleep(0)).then(responseBody),
  postForm: (url: string, formData: FormData) =>
    axios
      .post(url, formData, {
        headers: { "Content-type": "multipart/form-data" },
      })
      .then(responseBody),
  put: (url: string, body: {}) =>
    axios.put(url, body).then(sleep(0)).then(responseBody),
  putForm: (url: string, formData: FormData) =>
    axios
      .put(url, formData, {
        headers: { "Content-type": "multipart/form-data" },
      })
      .then(responseBody),
  delete: (url: string) => axios.delete(url).then(sleep(0)).then(responseBody),
  download: (url: string, name: string) =>
    axios.get(url, { responseType: "blob" }).then((response) => {
      const url = window.URL.createObjectURL(new Blob([response.data]));
      const link = document.createElement("a");
      link.href = url;
      link.setAttribute("download", name);
      document.body.appendChild(link);
      link.click();
    }),
};

export default requests;
