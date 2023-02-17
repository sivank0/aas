import { Box, Button, Container, Divider, IconButton, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Tooltip, Typography } from '@mui/material';
import React, { useEffect, useState } from 'react';
import { User } from '../../domain/users/user';
import { UsersProvider } from '../../domain/users/usersProvider';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import AddIcon from '@mui/icons-material/Add';
import { UserEditorModal } from './userEditorModal';

type ModalState = {
    isOpen: boolean;
    userId: string | null;
}

export const UsersPage = () => {
    const [users, setUsers] = useState<User[]>([]);
    const [modalState, setModalState] = useState<ModalState>({ isOpen: false, userId: null });

    useEffect(() => {
        async function init() {
            const users = await UsersProvider.getUsers();
            setUsers(users);
        }
        init();
    }, [])

    function changeModalState(isOpen: boolean = false, userId: string | null = null) {
        setModalState(state => ({ ...state, isOpen: isOpen, userId: userId }))
    }

    return (
        <Container maxWidth={false}>
            <Box sx={{ display: "flex", justifyContent: "space-between", alignItems: "center", marginBottom: 3 }}>
                <Typography variant="h5">
                    Пользователи
                </Typography>
                <Button startIcon={<AddIcon />} variant="outlined" onClick={() => { }}>
                    Добавить
                </Button>
            </Box>
            <TableContainer component={Paper}>
                <Table sx={{ minWidth: 650 }} aria-label="simple table">
                    <TableHead>
                        <TableRow>
                            <TableCell>Фамилия, имя, отчество</TableCell>
                            <TableCell>Email</TableCell>
                            <TableCell>Действия</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {
                            users.map(user =>
                                <TableRow
                                    key={user.id}
                                    sx={{ '&:last-child td, &:last-child th': { border: 0 } }} >
                                    <TableCell component="th" scope="row">
                                        {user.fullName}
                                    </TableCell>
                                    <TableCell>{user.email}</TableCell>
                                    <TableCell>
                                        <Box display="flex">
                                            <Tooltip title="Редактировать">
                                                <IconButton onClick={() => changeModalState(true, user.id)}>
                                                    <EditIcon />
                                                </IconButton>
                                            </Tooltip>
                                            <Tooltip title="Удалить">
                                                <IconButton onClick={() => { }}>
                                                    <DeleteIcon />
                                                </IconButton>
                                            </Tooltip>
                                        </Box>
                                    </TableCell>
                                </TableRow>
                            )
                        }
                    </TableBody>
                </Table>
            </TableContainer>
            {
                modalState.isOpen &&
                <UserEditorModal
                    userId={modalState.userId}
                    isOpen={modalState.isOpen}
                    onClose={changeModalState}
                />
            }
        </Container>
    )
}