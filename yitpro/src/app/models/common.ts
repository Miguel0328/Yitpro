export interface ISelected {
  id: number;
  ids: number[];
}

export interface IOption {
  key: string;
  text: string;
  value: number;
  image: {
    avatar: boolean;
    src: string;
  };
}
