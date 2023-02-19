import { Box, Button, Container, Divider, IconButton, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Tooltip, Typography } from '@mui/material';
import React, { useEffect, useState } from 'react';
import { User } from '../../domain/users/user';
import { UsersProvider } from '../../domain/users/usersProvider';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import AddIcon from '@mui/icons-material/Add';
import useDialog from '../../hooks/useDialog';
import { UserEditorModal } from './userEditorModal';
import { ConfirmDialogModal } from '../../sharedComponents/modals/modal';
import { Password } from '@mui/icons-material';
import { ChangePasswordModal } from './changePasswordModal';

export const UsersPage = () => {
    const [users, setUsers] = useState<User[]>([]);

    const userEditorModal = useDialog(UserEditorModal);
    const changePasswordModal = useDialog(ChangePasswordModal);
    const confirmationDialog = useDialog(ConfirmDialogModal);

    useEffect(() => {
        async function init() {
            const users = await UsersProvider.getUsers();
            setUsers(users);
        }
        init();
    }, [])

    function openUserEditorModal(userId: string | null = null) {
        userEditorModal.show({ userId });
    }

    function openChangePasswordModal(userId: string) {
        changePasswordModal.show({ userId });
    }

    async function removeUser(userId: string) {
        const removingUser = users.find(user => user.id === userId) ?? null;

        const isConfirmed = await confirmationDialog.show({ title: `Вы действительно хотите удалить пользователя: ${removingUser?.fullName}` })

        if (!isConfirmed) return;

        const result = await UsersProvider.removeUser(userId);

        if (!result.isSuccess) return;
    }

    return (
        <Container maxWidth={false}>
            <Box sx={{ display: "flex", justifyContent: "space-between", alignItems: "center", marginBottom: 3 }}>
                <Typography variant="h5">
                    Пользователи
                </Typography>
                <Button startIcon={<AddIcon />} variant="outlined" onClick={() => openUserEditorModal()}>
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
                                                <IconButton onClick={() => openUserEditorModal(user.id)}>
                                                    <EditIcon />
                                                </IconButton>
                                            </Tooltip>
                                            <Tooltip title="Удалить">
                                                <IconButton onClick={() => removeUser(user.id)}>
                                                    <DeleteIcon />
                                                </IconButton>
                                            </Tooltip>
                                            <Tooltip title="Изменить пароль">
                                                <IconButton onClick={() => openChangePasswordModal(user.id)}>
                                                    <Password />
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
        </Container>
    )
}