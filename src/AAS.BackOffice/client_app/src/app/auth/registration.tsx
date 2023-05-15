import {Box, Button, Link, Paper, Tooltip, Typography} from '@mui/material';
import {Container} from '@mui/system';
import React, {useState} from 'react';
import {useNavigate} from 'react-router-dom';
import {UserRegistrationBlank} from '../../domain/users/userRegistrationBlank';
import {AuthenticationProvider} from '../../domain/users/usersProvider';
import {InputForm} from '../../sharedComponents/inputs/inputForm';
import {AuthLinks} from './authLinks';

export const Registration = () => {
    const [userRegistrationBlank, setUserRegistrationBlank] = useState<UserRegistrationBlank>(UserRegistrationBlank.getDefault())

    const navigate = useNavigate()

    async function registerUser() {
        const result = await AuthenticationProvider.registerUser(userRegistrationBlank)

        if (!result.isSuccess) return alert(result.errors[0].message)

        alert('Удачно')
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
                    <Typography variant='h5'>
                        Регистрация
                    </Typography>
                    <Box sx={{display: "flex", gap: 2}}>
                        <InputForm
                            type="text"
                            label="Имя"
                            required
                            placeholder="Введите имя"
                            value={userRegistrationBlank.firstName}
                            onChange={(firstName) => setUserRegistrationBlank(blank => ({...blank, firstName}))}/>
                        <InputForm
                            type="text"
                            required
                            label="Фамилия"
                            placeholder="Введите фамилию"
                            value={userRegistrationBlank.lastName}
                            onChange={(lastName) => setUserRegistrationBlank(blank => ({...blank, lastName}))}/>
                    </Box>
                    <InputForm
                        type="text"
                        label="Отчество"
                        placeholder="Введите отчество"
                        value={userRegistrationBlank.middleName}
                        onChange={(middleName) => setUserRegistrationBlank(blank => ({...blank, middleName}))}/>
                    <InputForm
                        type="text"
                        required
                        label="Email"
                        placeholder="Введите email"
                        value={userRegistrationBlank.email}
                        onChange={(email) => setUserRegistrationBlank(blank => ({...blank, email}))}/>
                    <InputForm
                        type="text"
                        required
                        label="Телефон"
                        placeholder="Введите телефон"
                        value={userRegistrationBlank.phoneNumber}
                        onChange={(phoneNumber) => setUserRegistrationBlank(blank => ({...blank, phoneNumber}))}/>
                    <InputForm
                        type="password"
                        label="Пароль"
                        placeholder="Введите пароль"
                        value={userRegistrationBlank.password}
                        onChange={(password) => setUserRegistrationBlank(blank => ({...blank, password}))}/>
                    <InputForm
                        type="password"
                        label="Повторите пароль"
                        placeholder="Введите пароль"
                        value={userRegistrationBlank.rePassword}
                        onChange={(rePassword) => setUserRegistrationBlank(blank => ({...blank, rePassword}))}/>
                    <Tooltip title='Зарегистрироваться'>
                        <Button variant="outlined" onClick={() => registerUser()}>
                            Зарегистрироваться
                        </Button>
                    </Tooltip>
                    <Tooltip title='Перейти на страницу авторизации'>
                        <Link onClick={() => navigate(AuthLinks.authentification)}
                              sx={{
                                  fontSize: '15px',
                                  width: '30%',
                                  marginLeft: '79%'
                              }}>
                            Авторизация
                        </Link>
                    </Tooltip>
                </Paper>
            </Container>
        </Box>
    )
}