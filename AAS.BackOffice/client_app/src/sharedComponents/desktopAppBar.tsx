import * as React from 'react';
import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button';
import IconButton from '@mui/material/IconButton';
import MenuIcon from '@mui/icons-material/Menu';
import { Link } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import SystemUser from '../domain/systemUser';
import { useEffect, useState } from 'react';
import { UserRole } from '../domain/users/roles/userRole';
import { UsersProvider } from '../domain/users/usersProvider';

export const DesktopAppBar = () => {
    const [userRole, setUserRole] = useState<UserRole | null>(null);

    useEffect(() => {
        async function init() {
            const userRole = await UsersProvider.getUserRole(SystemUser.id);
            setUserRole(userRole);
        }
        init();
    }, [])

    return (
        <Box sx={{ flexGrow: 1 }}>
            <AppBar position="static">
                <Toolbar>
                    <IconButton
                        size="large"
                        edge="start"
                        color="inherit"
                        aria-label="menu"
                        sx={{ mr: 2 }}
                    >
                        <MenuIcon />
                    </IconButton>
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
        </Box>
    )
}