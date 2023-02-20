import * as React from 'react';
import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import IconButton from '@mui/material/IconButton';
import MenuIcon from '@mui/icons-material/Menu';
import SystemUser from '../domain/systemUser';
import { useEffect, useMemo, useState } from 'react';
import { UserRole } from '../domain/users/roles/userRole';
import { UsersProvider } from '../domain/users/usersProvider';
import { Sidebar } from './sidebar/sidebar';
import { default as SidebarModel } from '../tools/sidebar/sidebar';

export const DesktopAppBar = () => {
    const [userRole, setUserRole] = useState<UserRole | null>(null);
    const [isSidebarUncollapsed, setIsSidebarUncollapsed] = useState<boolean>(false);

    const isShowSidebar = useMemo(() => SidebarModel.items.length !== 0, [SidebarModel])

    useEffect(() => {
        async function init() {
            const userRole = await UsersProvider.getUserRole(SystemUser.id);
            setUserRole(userRole);
        }
        init();
    }, [])

    return (
        <Box sx={{ flexGrow: 1 }}>
            <AppBar sx={{ zIndex: 3, height: 65 }}>
                <Toolbar>
                    {
                        isShowSidebar &&
                        <IconButton
                            size="large"
                            edge="start"
                            color="inherit"
                            aria-label="menu"
                            sx={{ mr: 2 }}
                            onClick={() => setIsSidebarUncollapsed(!isSidebarUncollapsed)} >
                            <MenuIcon />
                        </IconButton>
                    }
                    <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                        Система приема заявок
                    </Typography>
                    <Box
                        sx={{
                            display: 'flex',
                            flexDirection: 'column',
                            gap: 0.5,
                            marginRight: '10px',
                            color: 'white'
                        }}>
                        <Typography>
                            {SystemUser.fullName}
                        </Typography>
                        <Typography>
                            {userRole?.name}
                        </Typography>
                    </Box>
                </Toolbar>
            </AppBar>
            {
                isShowSidebar &&
                <Sidebar
                    isSidebarUncollapsed={isSidebarUncollapsed}
                    changeIsNavigationOpen={(isUnCollapsed) => setIsSidebarUncollapsed(isUnCollapsed)} />
            }
        </Box>
    )
}