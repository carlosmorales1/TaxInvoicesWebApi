import { AbstractControl, ValidatorFn } from "@angular/forms";

export function integerValidator(): ValidatorFn {
  return (control: AbstractControl): { [key: string]: any } | null => {
    const valid = Number.isInteger(Number(control.value));

    return valid ? null : { 'notInteger': { value: control.value } };
  };
}