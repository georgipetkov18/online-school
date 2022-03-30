import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'hour'
})
export class HourPipe implements PipeTransform {

  transform(value: string | undefined, ...args: unknown[]): string | null {
    if (!value) {
      return null;
    }
    const lastColumnIndex = value.lastIndexOf(':');
    return value.substring(0, lastColumnIndex);
  }

}
