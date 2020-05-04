export default class {
    public username!: string;

    public password!: string;

    [key: string]: string;

    public properties = '[ "username", "password"]';

    constructor(username?: string, password?: string) {
        if (username != undefined && password != undefined) {
            this.username = username;
            this.password = password;
        }
    }
}