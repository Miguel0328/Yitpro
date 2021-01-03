import { IClasification, IPhase } from "../models/phase";
import requests from "./agent";

const Phase = {
  //    index: (): Promise<void> => requests.get("phase/index"),
  get: (): Promise<IPhase[]> => requests.get("phase"),
  getById: (id: number): Promise<IPhase> => requests.get("phase", id),
  getClasifications: (id: number): Promise<IClasification[]> =>
    requests.get("phase/clasifications", id),
  //   post: (phase: IPhase): Promise<number> => requests.post("phase", phase),
  put: (phase: IClasification): Promise<boolean> =>
    requests.put("phase", phase),
  putPSP: (phase: IPhase): Promise<boolean> => requests.put("phase/psp", phase),
  putAll: (id: number): Promise<boolean> => requests.put("phase", {}, id),
  //   putEnable: (phase: IPhase): Promise<boolean> => requests.put("phase", phase),
  //   download: (): Promise<void> =>
  //     requests.download("phase/download", "phases.xlsx"),
};

export default Phase;
