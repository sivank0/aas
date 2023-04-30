import {SidebarItem, mapToSidebarItems} from "./sidebarItem";

export default class Sidebar {
    public static items: SidebarItem[] = [];

    public static loadSidebar() {
        const sidebarLinksTree = (window as any).sidebarLinksTree;
        if (sidebarLinksTree == null) return;
        Sidebar.items = mapToSidebarItems(sidebarLinksTree);
    }
}

Sidebar.loadSidebar();