import { TeacherResponse } from "./teacher-response.model";

export interface TimetableEntryResponse {
    name: string,
    code: string,
    teacher: TeacherResponse,
    class: string,
    from: string,
    to: string,
}