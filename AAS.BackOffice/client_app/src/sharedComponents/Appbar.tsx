import { Margin, Visibility, VisibilityOff } from '@mui/icons-material';
import { Box, Button, IconButton, InputAdornment, Link, Paper, SvgIcon, TextField, Tooltip, Typography } from '@mui/material';
import { Container } from '@mui/system';
import React, { useState } from 'react';
import MenuIcon from '@mui/icons-material/Menu';

export const AppBar = () => {


    return (
        <Box sx={{
            display: 'flex',
            backgroundColor: '#1976d2',
            width: '100%',
            height: '5%',
        }}>
            <Box sx={{
                display: 'flex',
                flexDirection: 'row',
                color: 'white',
                alignItems: 'center',
                gap: 2,

            }}>
                <MenuIcon fontSize='large'
                    sx={{
                        marginBottom: '0.25%'
                    }} />
                <Typography>Окно окна</Typography>
                <Typography sx={{}}> // Перенести направо
                    Имя отчество
                </Typography>
            </Box>
        </Box>
    )
}