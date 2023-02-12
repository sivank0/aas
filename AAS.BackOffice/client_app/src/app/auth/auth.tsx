import { Margin, Visibility, VisibilityOff } from '@mui/icons-material';
import { Box, Button, IconButton, InputAdornment, Link, Paper, TextField, Tooltip, Typography } from '@mui/material';
import { Container } from '@mui/system';
import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';

export const Auth = () => {
    const [showPassword, setShowPassword] = useState<boolean>(false)
    const [email, setEmail] = useState<string | null>(null)
    const [password, setPassword] = useState<string | null>(null)

    const navigate = useNavigate()

    function changeEmail(event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) {
        const email = event.currentTarget.value ?? null;
        setEmail(email)
    }
    function changePassword(event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) {
        const password = event.currentTarget.value ?? null;
        setPassword(password)
    }

    return (
        <Container maxWidth={false}
            sx={{
                display: 'flex',
                flexDirection: 'column',
                height: '100%',
            }}>
            <Paper
                elevation={3}
                sx={{
                    display: 'flex',
                    flexDirection: 'column',
                    width: '25%',
                    minWidth: '450px',
                    minHeight: '300px',
                    padding: 2,
                    margin: 'auto',
                    gap: 2
                }}>
                <Typography variant='h5'>
                    Авторизация
                </Typography>

                <TextField
                    label="Email"
                    variant="standard"
                    value={email ?? ""}
                    onChange={changeEmail} />

                <TextField
                    label="Пароль"
                    variant="standard"
                    value={password ?? ""}
                    onChange={changePassword}
                    type={showPassword ? 'text' : 'password'}
                    InputProps={{
                        endAdornment: (
                            <InputAdornment position="end">
                                <Tooltip title='Показать пароль'>
                                    <IconButton
                                        aria-label="toggle password visibility"
                                        onClick={() => setShowPassword(!showPassword)}>
                                        {showPassword ? <VisibilityOff /> : <Visibility />}
                                    </IconButton>
                                </Tooltip>
                            </InputAdornment>
                        )
                    }} />
                <Tooltip title='Войти в аккаунт'>
                    <Button variant="outlined" onClick={() => { }}>
                        Войти
                    </Button>
                </Tooltip>
                <Box>
                    <Tooltip title='Перейти на страницу регистрации'>
                        <Link onClick={() => navigate('/registration')}
                            sx={{
                                fontSize: '15px',
                                width: '30%',
                                marginLeft: '79%'
                            }}>
                            Регистрация
                        </Link>
                    </Tooltip>
                </Box>
            </Paper>
        </Container>
    )
}