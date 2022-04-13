import { TimetableEntryRequest } from "./request/timetable-entry-request.model";
import { TimetableEntryResponse } from "./response/timetable-entry-response.model";

export class TimetableValue {
    constructor(
        public rowsToDisplay: string[],
        public entry: TimetableEntryRequest | TimetableEntryResponse,
    ) { };
}