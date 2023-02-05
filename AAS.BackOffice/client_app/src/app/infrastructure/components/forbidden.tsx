import React from 'react';
import { Box, Button, Container, Typography } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import LogoutIcon from '@mui/icons-material/Logout';
import KeyOffIcon from '@mui/icons-material/KeyOff';

export const Forbidden = () => {
    const navigate = useNavigate();

    return (
        <Container maxWidth={false} sx={{ height: "100%" }}>
            <Box sx={{
                display: "flex",
                flexDirection: "column",
                alignItems: "center",
                justifyContent: "center",
                height: "100%",
                gap: 3
            }}>
                <KeyOffIcon sx={theme => ({ fontSize: 95, color: theme.palette.error.main })} />
                <Typography variant='h3'>ДОСТУП ЗАПРЕЩЕН</Typography>
                <Box sx={{ display: "flex", gap: 3 }} >
                    <Button
                        variant='outlined'
                        onClick={() => window.location.href = '/'}>
                        На главную
                    </Button>
                    <Button
                        variant='outlined'
                        startIcon={<LogoutIcon />}
                        onClick={() => { }}>
                        Выйти
                    </Button>
                </Box>
            </Box>
        </Container>
    )

}