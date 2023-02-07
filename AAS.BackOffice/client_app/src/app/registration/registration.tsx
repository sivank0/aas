import { Visibility, VisibilityOff } from '@mui/icons-material';
import { Box, Button, IconButton, InputAdornment, Link, Paper, TextField, Tooltip, Typography } from '@mui/material';
import { Container } from '@mui/system';
import React, { useState } from 'react';

export const Registration = () => {
    const [showPassword, setShowPassword] = useState<boolean>(false)
    const [email, setEmail] = useState<string | null>(null)
    const [password, setPassword] = useState<string | null>(null)
    const [phone, setPhone] = useState<string | null>(null)
    const [name, setName] = useState<string | null>(null)
    const [lastName, setLastName] = useState<string | null>(null)
    const [middleName, setMiddleName] = useState<string | null>(null)

    function changeEmail(event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) {
        const email = event.currentTarget.value ?? null;
        setEmail(email)
    }
    function changePassword(event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) {
        const password = event.currentTarget.value ?? null;
        setPassword(password)
    }
    function changePhone(event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) {
        const phone = event.currentTarget.value ?? null;
        setPhone(phone);
    }
    function changeName(event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) {
        const name = event.currentTarget.value ?? null;
        setName(name);
    }
    function changeLastName(event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) {
        const lastName = event.currentTarget.value ?? null;
        setLastName(lastName);
    }
    function changeMiddleName(event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) {
        const middleName = event.currentTarget.value ?? null;
        setMiddleName(middleName);
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
                        value={name ?? ""}
                        onChange={changeName} />
                    <TextField
                        label='Фамилия'
                        variant='standard'
                        value={lastName ?? ""}
                        onChange={changeLastName} />
                </Box>

                <TextField // Не обязательное
                    label='Отчество'
                    variant='standard'
                    value={middleName ?? ""}
                    onChange={changeMiddleName} />

                <TextField
                    label="Email"
                    variant="standard"
                    value={email ?? ""}
                    onChange={changeEmail} />

                <TextField                      // НУЖНА ЛИБА ПОД ЭТО ДЕЛО.
                    label="Телефон"
                    variant="standard"
                    value={phone ?? ""}
                    onChange={changePhone} />


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
                <Tooltip title='Зарегистрировать аккаунт'>
                    <Button variant="outlined" onClick={() => { }}>
                        Зарегистрироваться
                    </Button>
                </Tooltip>

                <Box>
                    <Tooltip title='Перейти на страницу авторизации'>
                        <Link href=''
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