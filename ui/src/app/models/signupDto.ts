export class SignUpDto{
    constructor(emial: string, phone: string, password: string) {
        this.Email = emial;
        this.PhoneNumber = phone;
        this.Password = password;
    }

    Email: string | undefined;
    PhoneNumber: string | undefined;
    Password: string | undefined;
}