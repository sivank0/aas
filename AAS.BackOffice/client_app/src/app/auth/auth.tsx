import { Visibility, VisibilityOff } from '@mui/icons-material';
import { Box, Button, IconButton, InputAdornment, Paper, TextField } from '@mui/material';
import { Container } from '@mui/system';
import React, { useState } from 'react';

export const Auth = () => {
    const [showPassword, setShowPassword] = useState<boolean>(false)
    const [email, setEmail] = useState<string | null>(null)
    const [password, setPassword] = useState<string | null>(null)

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
                    padding: 2,
                    margin: 'auto',
                    gap: 2
                }}>
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
                                <IconButton
                                    aria-label="toggle password visibility"
                                    onClick={() => setShowPassword(!showPassword)}>
                                    {showPassword ? <VisibilityOff /> : <Visibility />}
                                </IconButton>
                            </InputAdornment>
                        )
                    }} />
                <Button variant="outlined" onClick={() => { }}>
                    Войти
                </Button>
            </Paper>
        </Container>
    )
}