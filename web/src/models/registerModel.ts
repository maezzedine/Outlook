export default class {
    public firstName!: string;

    public lastName!: string;

    public username!: string;

    public email!: string;
    
    public password!: string;

    [key: string]: string;

    public properties = '[ "firstName", "lastName", "username", "email", "password"]';
}