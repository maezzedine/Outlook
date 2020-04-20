import axios from 'axios';

const APP_URL = process.env.VUE_APP_OUTLOOK;
const API_URL = APP_URL + '/api/';

const BASE_URL = process.env.VUE_APP_BASE_URL;

export class Api {
    async getVolumeNumbers() {
        var response = await axios.get(API_URL + 'volumes');
        return response.data;
    }

    async getIssues(volumeId: Number) {
        var response = await axios.get(API_URL + 'issues/' + volumeId);
        return response.data;
    }

    async getCategories(issueID: Number) {
        var response = await axios.get(API_URL + 'categories/' + issueID);
        return response.data;
    }

    async getCategory(categoryID: Number, issueID: Number) {
        var response = await axios.get(API_URL + 'categories/' + categoryID + '/' + issueID);
        return response.data;
    }

    async getLanguageFile(lang: string | null) {
        if (lang == null) {
            return;
        }
        var response = await axios.get(BASE_URL +  lang + '.json');
        return response.data;
    }

    async getIcons() {
        var response = await axios.get(BASE_URL + 'font-awesome.json');
        return response.data;
    }

    async getColors() {
        var response = await axios.get(BASE_URL + 'category-color.json');
        return response.data;
    }

    async getArticles(issueId: Number) {
        var response = await axios.get(API_URL + 'articles/' + issueId);
        return response.data;
    }

    async getArticle(articleId: Number) {
        var response = await axios.get(API_URL + 'articles/article/' + articleId);
        return response.data;
    }

    async getTopArticles() {
        var response = await axios.get(API_URL + 'articles/');
        return response.data;
    }

    async getTopWriters() {
        var response = await axios.get(API_URL + 'members/top/');
        return response.data;
    }

    async getWriters() {
        var response = await axios.get(API_URL + 'members/');
        return response.data;
    }

    async getBoardMembers() {
        var response = await axios.get(API_URL + 'members/board');
        return response.data;
    }

    // Authorized
    async rateUpArticle(token: string, articleId: Number) {
        //if (token == null) {
        //    return Error("Unathorized action.");
        //}
        //var response = await axios.put(`${API_URL}articles/RateUpArticle/${articleId}`, {
        //    headers: {
        //        "Content-Type": "application/json",
        //        "x-functions-key": "{{auth}}",
        //        'Authorization': `Bearer ${token}`
        //    },
        //    redirect: 'follow'
            
        //});
        //return response.data;

        var myHeaders = new Headers();
        myHeaders.append("Content-Type", "application/json");
        myHeaders.append("x-functions-key", "{{auth}}");
        myHeaders.append("Authorization", `Bearer ${token}`);

        var requestOptions: RequestInit = {
            method: 'PUT',
            headers: myHeaders,
            redirect: 'follow'
        };

        return fetch(`${API_URL}articles/RateUpArticle/${articleId}`, requestOptions)
            .then(response => response.json())
            .catch(error => console.log('error', error));
    }
}

export const api = new Api();