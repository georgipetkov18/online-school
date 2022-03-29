import { TimetableEntryResponse } from "./response/timetable-entry-response.model";

export interface FullTimetable {
    monday?: TimetableEntryResponse[],
    tuesday?: TimetableEntryResponse[],
    wednesday?: TimetableEntryResponse[],
    thursday?: TimetableEntryResponse[],
    friday?: TimetableEntryResponse[],
}