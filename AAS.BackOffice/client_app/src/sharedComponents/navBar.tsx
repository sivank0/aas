import React, { useState } from "react";
import { Collapse, CSSObject, Link, List, ListItem, ListItemButton, ListItemIcon, ListItemText, styled, Theme } from "@mui/material";
import MuiDrawer from '@mui/material/Drawer';
import ExpandLess from '@mui/icons-material/ExpandLess';
import { ExpandMore } from "@mui/icons-material";
import { useNavigate } from "react-router-dom";

interface Props {
    isOpen: boolean
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


export const DesktopNavBar = (props: Props) => {

    const [isCollapsedOpen, setIsCollapsedOpen] = useState<boolean>(false);
    const navigate = useNavigate();

    function changeIsCollapsedOpen(isOpen: boolean) {
        setIsCollapsedOpen(isOpen);
    }

    function navigateTo(url: string) {
        navigate(url);
        props.changeIsNavigationOpen(false);
    }

    return (
        // <Drawer variant="permanent" sx={{ position: "fixed", zIndex: 2 }} open={props.isOpen}>
        //     <List sx={{ marginTop: 9 }} disablePadding>
        //         {SidebarModel.items.map(item => (
        //             item.innerItems.length == 0
        //                 ?
        //                 <ListItem key={item.id} disablePadding sx={{ display: 'block' }}>
        //                     <Link onClick={() => navigateTo(item.url)} sx={{
        //                         display: "flex",
        //                         flexDirection: "row",
        //                         textDecoration: "none",
        //                         justifyContent: 'center',
        //                         alignItems: "center",
        //                         color: "inherit",
        //                         width: "100%",
        //                         marginRight: props.isOpen ? 3 : 'auto'
        //                     }}>
        //                         <ListItemButton
        //                             sx={{
        //                                 minHeight: 48,
        //                                 justifyContent: props.isOpen ? 'initial' : 'center',
        //                                 px: 2.5,
        //                             }} >

        //                             <ListItemIcon sx={{ minWidth: 0 }}><SideBarIcon type={item.iconType} /></ListItemIcon>
        //                             {
        //                                 props.isOpen &&
        //                                 <ListItemText primary={item.text} sx={{ marginLeft: 2 }} />
        //                             }
        //                         </ListItemButton>
        //                     </Link>
        //                 </ListItem>
        //                 :
        //                 <ListItem key={item.id} disablePadding sx={{ display: 'block' }}>
        //                     <ListItemButton
        //                         sx={{
        //                             minHeight: 48,
        //                             justifyContent: props.isOpen ? 'initial' : 'center',
        //                             px: 2.5,
        //                         }} onClick={() => changeIsCollapsedOpen(!isCollapsedOpen)}>

        //                         <ListItemIcon sx={{ minWidth: 0 }}><SideBarIcon type={item.iconType} /></ListItemIcon>

        //                         {
        //                             props.isOpen &&
        //                             <ListItemText primary={item.text} sx={{ marginLeft: 2 }} />
        //                         }
        //                         {
        //                             props.isOpen &&
        //                             (isCollapsedOpen ? <ExpandLess /> : <ExpandMore />)
        //                         }
        //                     </ListItemButton>
        //                     {
        //                         item.innerItems.map(innerItem => (
        //                             <Collapse key={innerItem.id} in={isCollapsedOpen} timeout="auto" unmountOnExit sx={{ display: "flex", flexDirection: "row" }}>
        //                                 <List disablePadding sx={{ display: "flex", flexDirection: "row" }}>
        //                                     <Link onClick={() => navigateTo(innerItem.url)} sx={{
        //                                         display: "flex",
        //                                         flexDirection: "row",
        //                                         textDecoration: "none",
        //                                         justifyContent: 'center',
        //                                         alignItems: "center",
        //                                         color: "inherit",
        //                                         width: "100%"
        //                                     }}>
        //                                         <ListItemButton
        //                                             sx={{
        //                                                 minHeight: 48,
        //                                                 justifyContent: props.isOpen ? 'initial' : 'center',
        //                                                 px: 2.5,
        //                                             }} >
        //                                             <ListItemIcon sx={{ minWidth: 0 }}><SideBarIcon type={innerItem.iconType} /></ListItemIcon>
        //                                             {
        //                                                 props.isOpen &&
        //                                                 <ListItemText primary={innerItem.text} sx={{ marginLeft: 2 }} />
        //                                             }
        //                                         </ListItemButton>
        //                                     </Link>
        //                                 </List>
        //                             </Collapse>
        //                         ))
        //                     }
        //                 </ListItem>
        //         ))}
        //     </List>
        // </Drawer>
        <></>)
}