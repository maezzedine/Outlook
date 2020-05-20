import outlookUser from "../models/outlookUser";
import articleObject from '../models/articleObject';

export default interface state {
    lang: string;
    
    english: object;
    
    arabic: object;

    colors: object;

    user: outlookUser;

    article: articleObject;
}