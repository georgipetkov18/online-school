export class AuthenticateResponse {

    constructor(
        public username: string,
        public email: string,
        public role: string,
        public jwtToken: string,
    ) {};
}