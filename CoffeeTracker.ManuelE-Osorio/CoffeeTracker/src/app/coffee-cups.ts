import { Form, FormControl } from "@angular/forms";

export interface CoffeeCups {
    id: number;
    quantity: number;
    measure: number;
    description: string;
    units: CoffeeMeasureUnits;
    date: Date
}

export interface CoffeeCupsForm {
    id: FormControl<number>;
    quantity: FormControl<number>;
    measure: FormControl<number>;
    description: FormControl<string>;
    units: FormControl<CoffeeMeasureUnits>;
    date: FormControl<string>;
  }

export enum CoffeeMeasureUnits {
    ml = 0,
    oz = 1
}

export const CoffeeMeasureUnitsMapping: Record<string, string>= {
    [CoffeeMeasureUnits.ml]: "ml",
    [CoffeeMeasureUnits.oz]: "oz",
  }

export interface NotificationMessage{
  message : string;
  type: string;
}