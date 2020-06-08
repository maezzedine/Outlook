import axios from 'axios';
import LoginModel from '../models/loginModel';
import RegisterModel from '../models/registerModel';
import jwt from 'jsonwebtoken';
import outlookUser from '../models/outlookUser';
import store from '@/store/index';
import router from '@/router';
import ChangePasswordModel from '../models/changePasswordModel';

const APP_URL = process.env.VUE_APP_OUTLOOK;
const API_URL = process.env.VUE_APP_API_OUTLOOK;

const client_id = process.env.VUE_APP_AUTH_CLIENT_ID;
const scope = process.env.VUE_APP_AUTH_SCOPE;

export class AuthService {

    async Login(model: LoginModel) {
        var myHeaders = new Headers();
        myHeaders.append("Content-Type", "application/x-www-form-urlencoded");

        var urlencoded = new URLSearchParams();
        if (client_id != undefined && scope != undefined) {
            urlencoded.append("client_id", client_id);
            urlencoded.append("grant_type", "password");
            urlencoded.append("username", model.username);
            urlencoded.append("password", model.password);
            urlencoded.append("scope", scope);
        }

        var requestOptions: RequestInit = {
            method: 'POST',
            headers: myHeaders,
            body: urlencoded,
            redirect: 'follow'
        };

        var result = null;

        var response = await fetch(`${APP_URL}/connect/token`, requestOptions)
            .then(d => {
                if (!d.ok) {
                    throw d.json();
                }
                else {
                    d.json().then(r => {
                        var token = r.access_token;
                        var decodedToken: any = jwt.decode(token);
                        if (decodedToken != null) {
                            var expiration = new Date(decodedToken.exp * 1000);
                            var user = new outlookUser();
                            user.username = model.username;
                            user.token = token;
                            user.expirayDate = expiration;
                            localStorage.setItem('outlook-user', JSON.stringify(user));
                            store.dispatch('setUser', user);

                            var lang = router.currentRoute.params['lang'];
                            router.push(`/${lang}/`);
                        }
                    })
                }
            })

        return result;
    }

    async Register(model: RegisterModel) {
        var response = await axios.post(`${APP_URL}/api/identity/register`, model);
        store.dispatch('setUsername', model.username);
        return response.data;
    }

    // Authorized
    async ResendEmailVerification(username : string) {
        var token = store.getters.User.token;

        var myHeaders = new Headers();
        myHeaders.append("Content-Type", "application/json");
        myHeaders.append("Authorization", `Bearer ${token}`);

        var requestOptions: RequestInit = {
            method: 'POST',
            headers: myHeaders,
            redirect: 'follow',
        };

        return fetch(`${APP_URL}/api/identity/resendVerification/${username}`, requestOptions)
            .then(response => response.json())
            .catch(error => { if (process.env.NODE_ENV != 'production') console.log('error', error) });
    }

    // Authorized
    async getUser() {
        var token = store.getters.User.token;

        var myHeaders = new Headers();
        myHeaders.append("Content-Type", "application/json");
        myHeaders.append("Authorization", `Bearer ${token}`);

        var requestOptions: RequestInit = {
            method: 'POST',
            headers: myHeaders,
            redirect: 'follow'
        };

        return fetch(`${API_URL}/identity/getuser`, requestOptions)
            .then(response => response.json())
            .catch(error => { if (process.env.NODE_ENV != 'production') console.log('error', error) });
    }

    // Authorized
    async changePassword(model: ChangePasswordModel) {
        var token = store.getters.User.token;

        var myHeaders = new Headers();
        myHeaders.append("Content-Type", "application/json");
        myHeaders.append("Authorization", `Bearer ${token}`);

        var raw = JSON.stringify({ "oldPassword": model.oldPassword, "newPassword": model.newPassword });

        var requestOptions: RequestInit = {
            method: 'POST',
            headers: myHeaders,
            redirect: 'follow',
            body: raw
        };

        return fetch(`${API_URL}/identity/changepassword`, requestOptions)
            .then(response => response.json())
            .catch(error => { if (process.env.NODE_ENV != 'production') console.log('error', error) });
    }

    Logout() {
        store.dispatch('removeUser');
        localStorage.removeItem('outlook-user');
    }
}

export const authService = new AuthService();