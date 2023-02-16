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

export const DesktopAppBar = () => {
    const navigate = useNavigate()

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
                        Страница хз чего-то там...
                    </Typography>
                    <Link onClick={() => navigate('/authorization')}
                        sx={{
                            marginRight: '10px',
                            cursor: 'pointer',
                            color: 'white'
                        }}>Войти в аккаунт</Link>
                </Toolbar>
            </AppBar>
        </Box>
    )
}