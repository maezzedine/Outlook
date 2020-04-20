import axios, { AxiosRequestConfig } from 'axios';
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

        var response = axios({
            method: 'post',
            url: `${APP_URL}/connect/token`,
            data: urlencoded,
            headers: myHeaders
        });

        return response
            .then(d => { return d.data; })
            .catch(e => { return response; })
    }

    async Register(model: RegisterModel) {
        var response = await axios.post(`${APP_URL}/api/identity/register`, model).then(d => {
            if (d.data.succeeded) {
                var loginModel = new LoginModel(model.username, model.password);
                this.Login(loginModel).then(d => {
                    return d;
                })
            }
            else {
                console.log(d.data.errors);
                return d.data.errors;
            }
        })
    }
}

export const authService = new AuthService();