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
        <Box sx={{
            display: 'flex',
            flexDirection: 'row',
            height: '100%',
            width: '100%'
        }}>
            <Container sx={{
                display: 'flex',
                flexDirection: 'column',
                height: '100%',
            }}>
                <Paper
                    sx={{
                        display: 'flex',
                        flexDirection: 'column',
                        width: '25%',
                        minWidth: '800px',
                        minHeight: '300px',
                        padding: 2,
                        margin: 'auto',
                        gap: 2
                    }}>
                    <Typography gutterBottom>
                        Добро пожаловать на наш сайт! Мы - команда талантливых студентов, увлеченных разработкой
                        программного обеспечения, готовых принять вашу заявку на создание проекта любой сложности.
                    </Typography>
                    <Typography gutterBottom>
                        Мы понимаем, что каждый проект уникален и требует индивидуального подхода, поэтому мы тщательно
                        анализируем требования наших клиентов и работаем в тесном сотрудничестве с ними, чтобы
                        предоставить наилучшее решение, которое будет соответствовать их потребностям. Мы используем
                        современные технологии и методики разработки, что позволяет нам создавать качественное
                        программное обеспечение в срок и в рамках бюджета.
                    </Typography>
                    <Typography gutterBottom>
                        Мы ценим открытость и прозрачность в отношениях с нашими клиентами, поэтому вы можете быть
                        уверены, что мы будем держать вас в курсе всех этапов разработки вашего проекта. Мы также готовы
                        предоставить бесплатную консультацию и помощь в выборе технологий, если у вас возникнут вопросы.
                    </Typography>
                    <Typography gutterBottom>
                        Свяжитесь с нами, чтобы оставить заявку на разработку ПО. Мы готовы принять любые вызовы и
                        создать для вас программное обеспечение высокого уровня.
                    </Typography>
                </Paper>
            </Container>
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
        </Box>
    )
}