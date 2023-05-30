import { Box, Container, Paper, Typography } from '@mui/material';
import React, { useEffect, useMemo, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { EmailVerification } from '../../../domain/emailVerifications/emailVerification';
import { EmailVerificationsProvider } from '../../../domain/emailVerifications/emailVerificationsProvider';
import { User } from '../../../domain/users/user';
import { SuccessButton } from '../../../sharedComponents/buttons/button';
import { Logo } from '../../../sharedComponents/icons/logo';
import { AuthLinks } from '../../auth/authLinks';

type UserEmailVerification = {
    user: User,
    emailVerification: EmailVerification
}

export const EmailVerificationPage = () => {
    const params = useParams();
    const navigate = useNavigate();
    const userEmailVerificationToken = useMemo<string | null>(() => params.token ?? null, [params.token])
    const [userEmailVerification, setUserEmailVerification] = useState<UserEmailVerification | null>(null);

    useEffect(() => {
        async function init() {
            if (String.isNullOrWhitespace(userEmailVerificationToken)) return;

            const details = await EmailVerificationsProvider.getUserEmailVerification(userEmailVerificationToken);

            if (details === null) return;

            setUserEmailVerification({
                user: details.user,
                emailVerification: details.emailVerificaton
            });
        }

        init();

    }, [userEmailVerificationToken])

    async function confirmEmail() {
        if (userEmailVerification === null) return alert("Не найден пользователь, которому подтверждается почта");

        const result = await EmailVerificationsProvider.confirmEmail(userEmailVerificationToken);

        if (!result.isSuccess) return alert(result.errors[0].message);

        sessionStorage.setItem('emailVerificationMessage', 'Подтверждение почты произошло успешно! Вы можете начать пользоваться сервисом после прохождения процедуры авторизации');
        navigate(AuthLinks.authentification, { replace: true });
    }

    return (
        <Container
            maxWidth={false}
            sx={{
                width: '100%',
                height: '100%',
                display: 'flex',
                flexDirection: 'column',
                alignItems: 'center',
                justifyContent: 'center'
            }}>
            <Paper
                elevation={3}
                sx={{
                    display: 'flex',
                    flexDirection: 'column',
                    width: '30%',
                    height: '20%',
                    padding: 1
                }}>
                <Box
                    sx={{
                        display: 'flex',
                        justifyContent: 'center',
                        gap: 2
                    }}>
                    <Logo size={30} style={{ borderRadius: 1 }} />
                    <Typography variant='h6'>Система приема заявок</Typography>
                </Box>
                <Typography sx={{ marginY: 'auto' }}>
                    Подтверждение Email адреса: {userEmailVerification?.user.email}
                </Typography>
                <SuccessButton
                    onClick={() => confirmEmail()}
                    sx={{
                        marginTop: 'auto',
                        '& .MuiSvgIcon-root': {
                            fill: '#fff'
                        }
                    }} >
                    Подтвердить
                </SuccessButton>
            </Paper>
        </Container>
    )
}