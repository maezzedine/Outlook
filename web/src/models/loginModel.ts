export default class {
    public username!: string;

    public password!: string;

    constructor(username?: string, password?: string) {
        if (username != undefined && password != undefined) {
            this.username = username;
            this.password = password;
        }
    }
}