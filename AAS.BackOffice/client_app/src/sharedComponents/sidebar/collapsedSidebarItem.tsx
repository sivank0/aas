import {ExpandLess, ExpandMore} from '@mui/icons-material';
import {ListItem, ListItemButton, ListItemIcon, ListItemText, Collapse, List, Link} from '@mui/material';
import React, {useState} from 'react';
import {SidebarItem} from '../../tools/types/sidebar/sidebarItem';
import {SideBarIcon} from '../icons/sideBarIcon';

interface Props {
    sidebarItem: SidebarItem;
    isSidebarUncollapsed: boolean;
    navigateTo: (url: string) => void;
}

export const CollapsedSideBarItem = (props: Props) => {
    const [isCollapsedOpen, setIsCollapsedOpen] = useState<boolean>(false);

    function changeIsCollapsedOpen(isOpen: boolean) {
        setIsCollapsedOpen(isOpen);
    }

    return (
        <ListItem disablePadding sx={{display: 'block'}}>
            <ListItemButton
                sx={{
                    minHeight: 48,
                    justifyContent: props.isSidebarUncollapsed ? 'initial' : 'center',
                    px: 2.5,
                }} onClick={() => changeIsCollapsedOpen(!isCollapsedOpen)}>

                <ListItemIcon sx={{minWidth: 0}}><SideBarIcon type={props.sidebarItem.iconType}/></ListItemIcon>

                {
                    props.isSidebarUncollapsed &&
                    <ListItemText primary={props.sidebarItem.text} sx={{marginLeft: 2}}/>
                }
                {
                    props.isSidebarUncollapsed &&
                    (isCollapsedOpen ? <ExpandLess/> : <ExpandMore/>)
                }
            </ListItemButton>
            {
                props.sidebarItem.innerItems.map(innerItem => (
                    <Collapse key={innerItem.id} in={isCollapsedOpen} timeout="auto" unmountOnExit
                              sx={{display: "flex", flexDirection: "row"}}>
                        <List disablePadding sx={{display: "flex", flexDirection: "row"}}>
                            <Link onClick={() => props.navigateTo(innerItem.url)}
                                  sx={{
                                      display: "flex",
                                      flexDirection: "row",
                                      textDecoration: "none",
                                      justifyContent: 'center',
                                      alignItems: "center",
                                      color: "inherit",
                                      width: "100%"
                                  }}>
                                <ListItemButton
                                    sx={{
                                        minHeight: 48,
                                        justifyContent: props.isSidebarUncollapsed ? 'initial' : 'center',
                                        px: 2.5,
                                    }}>
                                    <ListItemIcon sx={{minWidth: 0}}><SideBarIcon
                                        type={innerItem.iconType}/></ListItemIcon>
                                    {
                                        props.isSidebarUncollapsed &&
                                        <ListItemText primary={innerItem.text} sx={{marginLeft: 2}}/>
                                    }
                                </ListItemButton>
                            </Link>
                        </List>
                    </Collapse>
                ))
            }
        </ListItem>
    )
}