export class AuthenticateRequest {
    
    constructor(
        public usernameOrEmail: string, 
        public password: string
    ) {};
}