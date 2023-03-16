import { Pipe, PipeTransform } from "@angular/core";
import { DateTime } from "luxon";

@Pipe({
    name: 'string2Date'
})
export class String2DatePipe implements PipeTransform {
    transform(value: string): string {
        return DateTime.fromISO(value).toLocaleString(DateTime.DATETIME_MED);
    }
}