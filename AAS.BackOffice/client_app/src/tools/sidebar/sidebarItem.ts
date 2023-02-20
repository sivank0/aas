import { SideBarIconType } from "./sideBarIconType";

export class SidebarItem {
    public id: string;
    public text: string;
    public url: string;
    public iconType: SideBarIconType;
    public innerItems: SidebarItem[];

    constructor(id: string, text: string, url: string, iconType: SideBarIconType, innerItems: SidebarItem[]) {
        this.id = id;
        this.text = text;
        this.url = url;
        this.iconType = iconType;
        this.innerItems = innerItems;
    }
}

const mapToSidebarItem = (data: any): SidebarItem => {
    const innerItems: SidebarItem[] = data.innerItems != null ? mapToSidebarItems(data.innerItems) : [];
    return new SidebarItem(data.id, data.text, data.url, data.iconType, innerItems);
};

export const mapToSidebarItems = (data: any[]): SidebarItem[] => {
    return data.map(d => mapToSidebarItem(d))
}
