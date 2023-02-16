import { Visibility, VisibilityOff } from '@mui/icons-material';
import { Box, Button, IconButton, InputAdornment, Link, Paper, TextField, Tooltip, Typography } from '@mui/material';
import { Container } from '@mui/system';
import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { UserRegistrationBlank } from '../../domain/users/userRegistrationBlank';
import { UsersProvider } from '../../domain/users/usersProvider';

export const Registration = () => {
    const [showPassword, setShowPassword] = useState<boolean>(false)
    const [showRePassword, setShowRePassword] = useState<boolean>(false)

    const [userRegistrationBlank, setUserRegistrationBlank] = useState<UserRegistrationBlank>(UserRegistrationBlank.getDefault())


    const navigate = useNavigate()

    function changeEmail(event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) {
        const email = event.currentTarget.value ?? null;
        setUserRegistrationBlank(blank => ({ ...blank, email }))
    }
    function changePassword(event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) {
        const password = event.currentTarget.value ?? null;
        setUserRegistrationBlank(blank => ({ ...blank, password }))
    }
    function changeRePassword(event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) {
        const rePassword = event.currentTarget.value ?? null;
        setUserRegistrationBlank(blank => ({ ...blank, rePassword }))
    }
    function changePhone(event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) {
        const phoneNumber = event.currentTarget.value ?? null;
        setUserRegistrationBlank(blank => ({ ...blank, phoneNumber }))
    }
    function changeName(event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) {
        const firstName = event.currentTarget.value ?? null;
        setUserRegistrationBlank(blank => ({ ...blank, firstName }))
    }
    function changeLastName(event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) {
        const lastName = event.currentTarget.value ?? null;
        setUserRegistrationBlank(blank => ({ ...blank, lastName }))
    }
    function changeMiddleName(event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) {
        const middleName = event.currentTarget.value ?? null;
        setUserRegistrationBlank(blank => ({ ...blank, middleName }))
    }

    async function registerUser() {
        const result = await UsersProvider.registerUser(userRegistrationBlank)
        if (!result.isSuccess) return console.log(result.errors[0].message)
        else alert('Удачно')
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
                    Регистрация
                </Typography>

                <Box>
                    <TextField
                        sx={{ marginRight: 2 }}
                        label='Имя'
                        variant='standard'
                        value={userRegistrationBlank.firstName ?? ""}
                        onChange={changeName} />
                    <TextField
                        label='Фамилия'
                        variant='standard'
                        value={userRegistrationBlank.lastName ?? ""}
                        onChange={changeLastName} />
                </Box>

                <TextField // Не обязательное
                    label='Отчество'
                    variant='standard'
                    value={userRegistrationBlank.middleName ?? ""}
                    onChange={changeMiddleName} />

                <TextField
                    label="Email"
                    variant="standard"
                    value={userRegistrationBlank.email ?? ""}
                    onChange={changeEmail} />

                <TextField                      // НУЖНА ЛИБА ПОД ЭТО ДЕЛО.
                    label="Телефон"
                    variant="standard"
                    value={userRegistrationBlank.phoneNumber ?? ""}
                    onChange={changePhone} />


                <TextField
                    label="Пароль"
                    variant="standard"
                    value={userRegistrationBlank.password ?? ""}
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
                <TextField
                    label="Повторите пароль"
                    variant="standard"
                    value={userRegistrationBlank.rePassword ?? ""}
                    onChange={changeRePassword}
                    type={showRePassword ? 'text' : 'password'}
                    InputProps={{
                        endAdornment: (
                            <InputAdornment position="end">
                                <Tooltip title='Показать пароль'>
                                    <IconButton
                                        aria-label="toggle password visibility"
                                        onClick={() => setShowRePassword(!showRePassword)}>
                                        {showRePassword ? <VisibilityOff /> : <Visibility />}
                                    </IconButton>
                                </Tooltip>
                            </InputAdornment>
                        )
                    }} />
                <Tooltip title='Зарегистрировать аккаунт'>
                    <Button variant="outlined" onClick={() => registerUser()}>
                        Зарегистрироваться
                    </Button>
                </Tooltip>

                <Box>
                    <Tooltip title='Перейти на страницу авторизации'>
                        <Link onClick={() => navigate('/authorization')}
                            sx={{
                                fontSize: '15px',
                                width: '30%',
                                marginLeft: '79%'
                            }}>
                            Авторизация
                        </Link>
                    </Tooltip>
                </Box>
            </Paper>
        </Container>
    )
}