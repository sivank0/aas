import { Box, Button, Link, Paper, Typography } from '@mui/material';
import { Container } from '@mui/system';
import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { AuthenticationProvider } from '../../domain/users/usersProvider';
import useDialog from '../../hooks/useDialog';
import { AboutUsCard } from "../../sharedComponents/cards/aboutUsCard";
import { InputForm } from '../../sharedComponents/inputs/inputForm';
import { AuthLinks } from './authLinks';
import { CanNotLogInHelpModal } from './helpers/canNotLogInHelpModal';

export const Auth = () => {
    const [email, setEmail] = useState<string | null>(null);
    const [password, setPassword] = useState<string | null>(null);
    const canNotLogInHelpModal = useDialog(CanNotLogInHelpModal);

    useEffect(() => {
        const emailVerificationMessage = sessionStorage.getItem('emailVerificationMessage');

        if (String.isNullOrWhitespace(emailVerificationMessage)) return;

        alert(emailVerificationMessage);
    }, [sessionStorage.getItem('emailVerificationMessage')])

    const navigate = useNavigate()

    async function logIn() {
        const result = await AuthenticationProvider.logIn(email, password);

        if (!result.isSuccess) return alert(result.errors[0].message);

        window.location.href = "/";
    }

    return (
        <Box sx={{
            display: 'flex',
            flexDirection: 'row',
            height: '100%',
            width: '100%'
        }}>
            <AboutUsCard />
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
                        onChange={(email) => setEmail(email)} />
                    <InputForm
                        type="password"
                        label="Пароль"
                        placeholder="Введите пароль"
                        value={password}
                        onChange={(password) => setPassword(password)} />
                    <Button variant="outlined" onClick={logIn}>
                        Войти
                    </Button>
                    <Box sx={{ display: 'flex', justifyContent: 'space-between' }}>
                        <Link onClick={() => canNotLogInHelpModal.show({})}
                            sx={{ fontSize: 15 }}>
                            Не могу войти
                        </Link>
                        <Link onClick={() => navigate(AuthLinks.registration)}
                            sx={{ fontSize: 15 }}>
                            Регистрация
                        </Link>
                    </Box>
                </Paper>
            </Container>
        </Box>
    )
}