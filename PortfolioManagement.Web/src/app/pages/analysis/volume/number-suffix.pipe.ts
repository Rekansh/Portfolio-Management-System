import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'numberSuffix'
})
export class NumberSuffixPipe implements PipeTransform {

  transform(value: number, decimalPlaces: number = 1): string {
    if (value === null || value === undefined) {
      return '';
    }

    let suffix = '';
    let formattedValue = value;

    if (value >= 1_00_00_000) { // 1 crore or more
      suffix = 'Cr';
      formattedValue = value / 1_00_00_000;
    } else if (value >= 1_00_000) { // 1 lakh or more
      suffix = 'L';
      formattedValue = value / 1_00_000;
    } else if (value >= 1_000) { // 1 thousand or more
      suffix = 'K';
      formattedValue = value / 1_000;
    }

    return `${formattedValue.toFixed(decimalPlaces)}${suffix}`;
  }
}
