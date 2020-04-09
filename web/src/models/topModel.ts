import { ApiObject } from './apiObject';

export default class TopModel {
    public title: string;

    public titleIcon: string;

    public itemIcon: string;

    public items: Array<ApiObject>;

    public itemTitle: string;

    public field: string;

    constructor(title: string, titleIcon: string, itemIcon: string, items: Array<ApiObject>, itemTitle: string, field: string) {
        this.title = title;
        this.titleIcon = titleIcon;
        this.itemIcon = itemIcon;
        this.items = items;
        this.itemTitle = itemTitle;
        this.field = field;
    }

    public setTitle(title: string) {
        this.title = title;
    }
}