import {Link, ListItem, ListItemButton, ListItemIcon, ListItemText} from '@mui/material';
import React from 'react';
import {SidebarItem} from '../../tools/types/sidebar/sidebarItem';
import {SideBarIcon} from '../icons/sideBarIcon';

interface Props {
    sidebarItem: SidebarItem;
    isSidebarUncollapsed: boolean;
    navigateTo: (url: string) => void;
}

export const SingleSidebarItem = (props: Props) => {
    return (
        <ListItem disablePadding sx={{display: 'block'}}>
            <Link
                onClick={() => props.navigateTo(props.sidebarItem.url)}
                sx={{
                    display: "flex",
                    flexDirection: "row",
                    textDecoration: "none",
                    justifyContent: 'center',
                    alignItems: "center",
                    color: "inherit",
                    width: "100%",
                    marginRight: props.isSidebarUncollapsed ? 3 : 'auto'
                }}>
                <ListItemButton
                    sx={{
                        minHeight: 48,
                        justifyContent: props.isSidebarUncollapsed ? 'initial' : 'center',
                        px: 2.5,
                    }}>
                    <ListItemIcon sx={{minWidth: 0}}><SideBarIcon type={props.sidebarItem.iconType}/></ListItemIcon>
                    {
                        props.isSidebarUncollapsed &&
                        <ListItemText primary={props.sidebarItem.text} sx={{marginLeft: 2}}/>
                    }
                </ListItemButton>
            </Link>
        </ListItem>
    )
}