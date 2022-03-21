export class RegisterRequest {
    constructor(
        public username: string,
        public password: string,
        public firstName: string,
        public lastName: string,
        public email: string,
        public roleName: string,
        public classId?: string,
        public subjectId?: string,
    ) { };
}