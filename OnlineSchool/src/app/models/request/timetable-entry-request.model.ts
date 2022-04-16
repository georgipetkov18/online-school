export class TimetableEntryRequest {
    constructor(
        public dayOfWeek: 'Monday' | 'Tuesday' | 'Wednesday' | 'Thursday' | 'Friday',
        public subjectId: string,
        public lessonId: string,
        public classId: string,
        public teacherId: string,
        public timetableEntryId: string | null = null
    ) {}
}