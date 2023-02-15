import { Margin, Visibility, VisibilityOff } from '@mui/icons-material';
import { Box, Button, IconButton, InputAdornment, Link, Paper, TextField, Tooltip, Typography } from '@mui/material';
import { Container } from '@mui/system';
import React, { useState } from 'react';
import { Route, Routes, useNavigate } from 'react-router-dom';
import { UserAuthorizationBlank as UserAuthorizationBlank } from '../../domain/users/userAuthorizationBlank';
import { UsersProvider, UserViewBlank } from '../../domain/users/usersProvider';
import { UserPage } from '../users/userPage';

export const Auth = () => {
    const [showPassword, setShowPassword] = useState<boolean>(false)
    const [userAuthorizationBlank, setUserAuthorizationBlank] = useState<UserAuthorizationBlank>(UserAuthorizationBlank.getDefault())
    const [user, setUser] = useState<UserViewBlank>(UserViewBlank.getDefault())
    const navigate = useNavigate()

    function changeEmail(event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) {
        const email = event.currentTarget.value ?? null;
        setUserAuthorizationBlank(blank => ({ ...blank, email }))
    }
    function changePassword(event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) {
        const password = event.currentTarget.value ?? null;
        setUserAuthorizationBlank(blank => ({ ...blank, password }))
    }

    async function authorizeUser() {
        const user = await UsersProvider.authorizationUser(userAuthorizationBlank)
        if (user == null) return alert('Ошибка')
        setUser(user);
        return navigate('/user')
    }
    <Routes>
        <Route path='/user' element={<UserPage firstName={user.firstName} middleName={user.middleName} lastName={user.lastName} email={user.email} phoneNumber={user.phoneNumber} />} />
    </Routes>
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
                    value={userAuthorizationBlank.email ?? ""}
                    onChange={changeEmail} />

                <TextField
                    label="Пароль"
                    variant="standard"
                    value={userAuthorizationBlank.password ?? ""}
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