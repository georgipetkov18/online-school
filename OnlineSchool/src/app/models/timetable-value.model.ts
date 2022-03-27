import { TimetableEntryRequest } from "./request/timetable-entry-request.model";

export class TimetableValue {
    constructor(
        public rowsToDisplay: string[],
        public entry: TimetableEntryRequest,
    ) { };
}