import { TimetableEntryResponse } from "./response/timetable-entry-response.model";

export interface FullTimetable {
    Monday?: TimetableEntryResponse[],
    Tuesday?: TimetableEntryResponse[],
    Wednesday?: TimetableEntryResponse[],
    Thursday?: TimetableEntryResponse[],
    Friday?: TimetableEntryResponse[],
}