import axios from 'axios';
import store from '../store';

const API_URL = process.env.VUE_APP_API_OUTLOOK;
const BASE_URL = process.env.VUE_APP_BASE_URL;

export class Api {
    async Get(page: string, params?: Array<any>) {
        var url = `${API_URL}/${page}`;
        if (params != undefined) {
            for (var param of params) {
                url += `/${param}`;
            }
        }
        var response = await axios.get(url);
        return response.data;
    }

    async AuthorizedAction(method: string, page: string, params?: Array<any>, body?: string) {
        var myHeaders = new Headers();
        myHeaders.append("Content-Type", "application/json");
        myHeaders.append("Authorization", `Bearer ${store.getters.User.token}`);

        var requestOptions: RequestInit = {
            method: method,
            headers: myHeaders,
            redirect: 'follow',
            body: body
        };

        var url = `${API_URL}/${page}`;
        if (params != undefined) {
            for (var param of params) {
                url += `/${param}`;
            }
        }

        return fetch(url, requestOptions)
            .then(response => response.json())
            .catch(error => { if (process.env.NODE_ENV != 'production') console.log('error', error) });
    }

    async getLocalJsonFile(file: string | null) {
        if (file == null) {
            return;
        }

        var response = await axios.get(`${BASE_URL}/${file}.json`);
        return response.data;
    }

    async postFile(page: string, file: FormData) {
        var response = await axios.post(`${API_URL}/${page}`, file, {
            headers: {
                'Content-Type': 'multipart/form-data',
                "Authorization": `Bearer ${store.getters.User.token}`
            }
        });
        return response.status;
    }
}

export const api = new Api();