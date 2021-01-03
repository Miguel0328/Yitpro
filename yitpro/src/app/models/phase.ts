export interface IPhase {
    id: number;
    name: string;
    psp: boolean;
    active: boolean;
}

export interface IClasification {
    id: number;
    phaseId: number;
    name: string;
    active: boolean;
}