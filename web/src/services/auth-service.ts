import axios from 'axios';
import LoginModel from '../models/loginModel';
import RegisterModel from '../models/registerModel';

const APP_URL = process.env.VUE_APP_OUTLOOK;
const client_id = process.env.VUE_APP_AUTH_CLIENT_ID;
const scope = process.env.VUE_APP_AUTH_SCOPE;

export class AuthService {

    async Login(model: LoginModel) {
        var myHeaders = new Headers();
        myHeaders.append("Content-Type", "application/x-www-form-urlencoded");

        var urlencoded = new URLSearchParams();
        urlencoded.append("client_id", client_id);
        urlencoded.append("grant_type", "password");
        urlencoded.append("username", model.username);
        urlencoded.append("password", model.password);
        urlencoded.append("scope", scope);

        var requestOptions: RequestInit = {
            method: 'POST',
            headers: myHeaders,
            body: urlencoded,
            redirect: 'follow'
        };

        var result!: Promise<JSON>;

        var response = await fetch("https://localhost:5000/connect/token", requestOptions)
            .then(d => {
                if (!d.ok) {
                    throw d.json();
                }
                else {
                    result = d.json();
                }
            })

        return result;
    }

    async Register(model: RegisterModel) {
        var response = await axios.post(`${APP_URL}/api/identity/register`, model);
        return response.data;
    }
}

export const authService = new AuthService();