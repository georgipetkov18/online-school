import { TeacherResponse } from "./teacher-response.model";

export interface TimetableEntryResponse {
    timetableEntryId: string,
    name: string,
    code: string,
    teacher: TeacherResponse,
    class: string,
    from: string,
    to: string,
}