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

    async getTopArticles() {
        var response = await axios.get(API_URL + 'articles/');
        return response.data;
    }

    async getTopWriters() {
        var response = await axios.get(API_URL + 'members/top/');
        return response.data;
    }
}

export const api = new Api();