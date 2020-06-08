import { HubConnectionBuilder, LogLevel } from '@aspnet/signalr';
import vue, { PluginObject } from 'vue';
import ArticleHub from './Models/articleHub';

//const API_URL = process.env.VUE_APP_API_OUTLOOK;


const install = (Vue: typeof vue): void => {
    // use new Vue instance as an event bus
    const articleHub = new ArticleHub(Vue);
    // every component will use this.$articleHub to access the event bus
    Vue.prototype.$articleHub = articleHub;

    Object.defineProperties(Vue.prototype, {
        $articleHub: {
            get() {
                return articleHub;
            }
        }
    })
}

const plugin: PluginObject<any> = {
    install
}

export default plugin;