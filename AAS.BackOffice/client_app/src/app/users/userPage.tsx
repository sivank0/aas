import { Box, Button, Typography } from '@mui/material';
import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { UserViewBlank } from '../../domain/users/UserViewBlank';

export const UserPage = (userViewBlank: UserViewBlank) => {


    const navigate = useNavigate()

    return (
        <Box>
            <Typography>
                {userViewBlank.email}
            </Typography>
            <Typography>
                {userViewBlank.lastName}
            </Typography>
            <Typography>
                {userViewBlank.firstName}
            </Typography>
            <Typography>
                {userViewBlank.middleName}
            </Typography>
            <Typography>
                {userViewBlank.phoneNumber}
            </Typography>
        </Box>
    )
}