import { ApiObject } from './apiObject';

export default class TopModel {
    public title: string;

    public titleIcon: string;

    public itemIcon: string;

    public items: Array<ApiObject>;

    public itemTitle: string;

    public field: string;

    public component: string;

    constructor(title: string, titleIcon: string, itemIcon: string, items: Array<ApiObject>, itemTitle: string, field: string, component: string) {
        this.title = title;
        this.titleIcon = titleIcon;
        this.itemIcon = itemIcon;
        this.items = items;
        this.itemTitle = itemTitle;
        this.field = field;
        this.component = component;
    }

    public setTitle(title: string) {
        this.title = title;
    }
}