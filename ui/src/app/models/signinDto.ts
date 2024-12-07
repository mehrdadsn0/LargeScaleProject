export class SignInDto{
    constructor(emial: string, password: string) {
        this.Email = emial;
        this.Password = password;
    }

    Email: string | undefined;
    Password: string | undefined;
}