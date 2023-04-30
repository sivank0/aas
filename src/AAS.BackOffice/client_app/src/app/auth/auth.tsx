import {Box, Button, Link, Paper, Typography} from '@mui/material';
import {Container} from '@mui/system';
import React, {useState} from 'react';
import {useNavigate} from 'react-router-dom';
import {AuthenticationProvider} from '../../domain/users/usersProvider';
import {InputForm} from '../../sharedComponents/inputs/inputForm';
import {AuthLinks} from './authLinks';

export const Auth = () => {
    const [email, setEmail] = useState<string | null>(null);
    const [password, setPassword] = useState<string | null>(null);

    const navigate = useNavigate()

    async function logIn() {
        const result = await AuthenticationProvider.logIn(email, password);

        if (!result.isSuccess) return alert(result.errors[0].message);

        window.location.href = "/";
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
                <Typography variant='h5'>Авторизация</Typography>
                <InputForm
                    type="text"
                    label="Email"
                    placeholder="Введите email"
                    value={email}
                    onChange={(email) => setEmail(email)}/>
                <InputForm
                    type="password"
                    label="Пароль"
                    placeholder="Введите пароль"
                    value={password}
                    onChange={(password) => setPassword(password)}/>
                <Button variant="outlined" onClick={logIn}>
                    Войти
                </Button>
                <Link onClick={() => navigate(AuthLinks.registration)}
                      sx={{
                          fontSize: '15px',
                          width: '30%',
                          marginLeft: '79%'
                      }}>
                    Регистрация
                </Link>
            </Paper>
        </Container>
    )
}