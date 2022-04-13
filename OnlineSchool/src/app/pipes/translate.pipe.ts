import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'translate'
})
export class TranslatePipe implements PipeTransform {

  transform(value: string, ...args: unknown[]) {
    switch (value.toLowerCase()) {
      case 'monday':
        return 'Понеделник';

      case 'tuesday':
        return 'Вторник';

      case 'wednesday':
        return 'Сряда';

      case 'thursday':
        return 'Четвъртък';

      case 'friday':
        return 'Петък';

      default:
        return null;
    }
  }

}
