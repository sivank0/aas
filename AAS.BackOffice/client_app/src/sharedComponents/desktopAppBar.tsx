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
import { AuthenticationProvider, UsersProvider } from '../domain/users/usersProvider';
import { Sidebar } from './sidebar/sidebar';
import { default as SidebarModel } from '../tools/sidebar/sidebar';
import { Button, Link, Menu, MenuItem } from '@mui/material';
import { BrowserType } from '../tools/browserType/browserType';
import PersonIcon from '@mui/icons-material/Person';
import LogoutIcon from '@mui/icons-material/Logout';
import { useNavigate } from 'react-router-dom';

type MenuState = {
    isOpen: boolean;
    achorEl: HTMLElement | null;
}

export const DesktopAppBar = () => {
    const [userRole, setUserRole] = useState<UserRole | null>(null);
    const [isSidebarUncollapsed, setIsSidebarUncollapsed] = useState<boolean>(false);
    const [menuState, setMenuState] = useState<MenuState>({ isOpen: false, achorEl: null });

    const isShowSidebar = useMemo(() => SidebarModel.items.length !== 0, [SidebarModel])

    const navigate = useNavigate();

    useEffect(() => {
        async function init() {
            const userRole = await UsersProvider.getUserRole(SystemUser.id);
            setUserRole(userRole);
        }
        init();
    }, [])

    function changeMenuState(isOpen: boolean = false, event: React.MouseEvent<HTMLButtonElement, MouseEvent> | null = null) {
        const achorEl = event?.currentTarget ?? null;

        setMenuState({ isOpen, achorEl });
    }

    function navigateToProfile() {
        navigate("/");
    }

    async function logOut() {
        await AuthenticationProvider.logOut();

        window.location.reload();
    }

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
                    <Button variant="text" onClick={(event) => changeMenuState(true, event)}>
                        <Box
                            sx={{
                                display: 'flex',
                                flexDirection: 'column',
                                gap: 0.5,
                                marginRight: '10px',
                                color: 'white'
                            }}>
                            <Typography sx={{ textTransform: "none" }}>
                                {SystemUser.fullName}
                            </Typography>
                            <Typography sx={{ textTransform: "none" }}>
                                {userRole?.name}
                            </Typography>
                        </Box>
                    </Button>
                </Toolbar>
            </AppBar>
            {
                isShowSidebar &&
                <Sidebar
                    isSidebarUncollapsed={isSidebarUncollapsed}
                    changeIsNavigationOpen={(isUnCollapsed) => setIsSidebarUncollapsed(isUnCollapsed)} />
            }
            <Menu
                keepMounted
                open={menuState.isOpen}
                onClose={() => changeMenuState()}
                anchorEl={menuState.achorEl}
                anchorOrigin={{
                    vertical: 'top',
                    horizontal: 'center',
                }}
                transformOrigin={{
                    vertical: 'top',
                    horizontal: 'center',
                }}
                sx={{
                    mt: window.browserType === BrowserType.Mobile ? 0 : '50px',
                    "& .MuiPopover-paper": {
                        width: 200
                    }
                }}>
                <Link onClick={navigateToProfile} sx={{
                    textDecoration: "none",
                    color: "#000"
                }}>
                    <MenuItem sx={{ gap: 1 }}>
                        <PersonIcon />
                        <Typography component="span">Профиль</Typography>
                    </MenuItem>
                </Link>
                <MenuItem onClick={logOut} sx={{ gap: 1 }}>
                    <LogoutIcon />
                    <Typography component="span">Выйти</Typography>
                </MenuItem>
            </Menu>
        </Box>
    )
}