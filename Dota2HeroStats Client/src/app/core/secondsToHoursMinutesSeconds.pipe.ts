import { Pipe } from "@angular/core";
@Pipe({
    name: "secondsToHoursMinutesSeconds"
})
export class D2SecondsToHoursMinutesSecondsPipe {
    transform(value: any): string {
        let valueNumber = Number.parseInt(value);
        let hours = Math.floor(valueNumber / 3600);
        let mins = Math.floor((valueNumber % 3600) / 60);
        let secs = Math.floor(valueNumber % 60);

        return (hours > 0 ? hours + ":" : "") + this.formatMinSec(mins) + ":" + this.formatMinSec(secs);
    }

    formatMinSec(val: number): string {
        return val > 9 ? "" + val : "0" + val;
    }
}