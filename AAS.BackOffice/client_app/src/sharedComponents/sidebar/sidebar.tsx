import React from "react";
import { CSSObject, List, styled, Theme } from "@mui/material";
import { default as SidebarModel } from '../../tools/types/sidebar/sidebar';
import MuiDrawer from '@mui/material/Drawer';
import { useNavigate } from "react-router-dom";
import { SingleSidebarItem } from "./singleSidebarItem";
import { CollapsedSideBarItem } from "./collapsedSidebarItem";

interface Props {
    isSidebarUncollapsed: boolean
    changeIsNavigationOpen: (isOpen: boolean) => void;
}
const drawerWidth = 240;

const openedMixin = (theme: Theme): CSSObject => ({
    width: drawerWidth,
    transition: theme.transitions.create('width', {
        easing: theme.transitions.easing.sharp,
        duration: theme.transitions.duration.enteringScreen,
    }),
    overflowX: 'hidden',
});

const closedMixin = (theme: Theme): CSSObject => ({
    transition: theme.transitions.create('width', {
        easing: theme.transitions.easing.sharp,
        duration: theme.transitions.duration.leavingScreen,
    }),
    overflowX: 'hidden',
    width: `calc(${theme.spacing(7)} + 1px)`,
    [theme.breakpoints.up('sm')]: {
        width: `calc(${theme.spacing(8)} + 1px)`,
    },
});

const Drawer = styled(MuiDrawer, { shouldForwardProp: (prop) => prop !== 'open' })(
    ({ theme, open }) => ({
        width: drawerWidth,
        flexShrink: 0,
        whiteSpace: 'nowrap',
        boxSizing: 'border-box',
        ...(open && {
            ...openedMixin(theme),
            '& .MuiDrawer-paper': openedMixin(theme),

        }),
        ...(!open && {
            ...closedMixin(theme),
            '& .MuiDrawer-paper': closedMixin(theme),
        }),
    }),
);

export const Sidebar = (props: Props) => {
    const navigate = useNavigate();

    function navigateTo(url: string) {
        navigate(url);
        props.changeIsNavigationOpen(false);
    }

    return (
        <Drawer variant="permanent" sx={{ position: "fixed", zIndex: 2 }} open={props.isSidebarUncollapsed}>
            <List sx={{ marginTop: 9 }} disablePadding>
                {
                    SidebarModel.items.map(item => (
                        item.innerItems.length == 0
                            ?
                            <SingleSidebarItem
                                sidebarItem={item}
                                isSidebarUncollapsed={props.isSidebarUncollapsed}
                                navigateTo={(url) => navigateTo(url)} />
                            :
                            <CollapsedSideBarItem
                                sidebarItem={item}
                                isSidebarUncollapsed={props.isSidebarUncollapsed}
                                navigateTo={(url) => navigateTo(url)} />
                    ))
                }
            </List>
        </Drawer>
    )
}