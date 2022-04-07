import { TimetableEntryResponse } from "./timetable-entry-response.model";

export interface TimetableEntriesInfo {
    current?: TimetableEntryResponse,
    next?: TimetableEntryResponse,
}