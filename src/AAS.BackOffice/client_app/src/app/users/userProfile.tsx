import {
    Box,
    Button,
    Container,
    Divider,
    IconButton,
    Link,
    Paper,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
    Tooltip,
    Typography
} from '@mui/material';
import React, {useEffect, useState} from 'react';
import {User} from '../../domain/users/user';
import {UsersProvider} from '../../domain/users/usersProvider';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import AddIcon from '@mui/icons-material/Add';
import useDialog from '../../hooks/useDialog';
import {UserEditorModal} from './userEditorModal';
import {ConfirmDialogModal} from '../../sharedComponents/modals/modal';
import {Password} from '@mui/icons-material';
import {ChangePasswordModal} from './changePasswordModal';
import SystemUser from '../../domain/systemUser';

export const UserProfile = () => {
    const [user, setUser] = useState<User | null>(null);
    const [isInit, setIsInit] = useState<boolean>(false);

    const userEditorModal = useDialog(UserEditorModal);
    const changePasswordModal = useDialog(ChangePasswordModal);
    const confirmationDialog = useDialog(ConfirmDialogModal);

    useEffect(() => {
        async function init() {
            const user = await UsersProvider.getUserById(SystemUser.id);
            setUser(user!);
            setIsInit(true);
        }

        init();
    })

    function openUserEditorModal(userId: string | null) {
        if (userId === null) return;
        userEditorModal.show({userId});
    }

    function openChangePasswordModal(userId: string | null) {
        if (userId === null) return;
        changePasswordModal.show({userId});
    }

    return (
        <Box sx={{
            display: 'flex',
            justifyContent: 'center',
            width: '100%',
            minHeight: '500px'
        }}>
            <Paper elevation={3}
                   sx={{width: '560px'}}>
                <Box sx={{
                    padding: '10px'
                }}>
                    <Box sx={{
                        display: 'flex',
                        justifyContent: 'space-between',
                        flexDirection: 'row'
                    }}>
                        <Typography sx={{
                            fontSize: '20pt'
                        }}>
                            Профиль
                        </Typography>
                        <IconButton onClick={() => openUserEditorModal(user?.id ?? null)}>
                            <EditIcon/>
                        </IconButton>
                    </Box>
                    <Divider sx={{marginY: 1}}/>
                    <Box sx={{
                        display: 'flex',
                        flexDirection: 'column'
                    }}>
                        <Box sx={{
                            display: 'flex',
                            flexDirection: 'row',
                            justifyContent: 'space-between'
                        }}>
                            <Typography>
                                Имя:
                            </Typography>
                            <Typography>
                                {user?.firstName}
                            </Typography>
                        </Box>
                        <Box sx={{
                            display: 'flex',
                            flexDirection: 'row',
                            justifyContent: 'space-between'
                        }}>
                            <Typography>
                                Фамилия:
                            </Typography>
                            <Typography>
                                {user?.lastName}
                            </Typography>
                        </Box>
                        {user?.middleName != null &&
                            <Box sx={{
                                display: 'flex',
                                flexDirection: 'row',
                                justifyContent: 'space-between'
                            }}>
                                <Typography>
                                    Отчество:
                                </Typography>
                                <Typography>
                                    {user?.middleName}
                                </Typography>
                            </Box>
                        }
                        <Divider sx={{marginY: 1}}/>

                        <Box sx={{
                            display: 'flex',
                            flexDirection: 'row',
                            justifyContent: 'space-between'
                        }}>
                            <Typography>
                                Email:
                            </Typography>
                            <Typography>
                                {user?.email}
                            </Typography>
                        </Box>
                        <Link sx={{
                            cursor: 'pointer',
                            display: 'flex',
                            marginLeft: 'auto'
                        }}
                              onClick={() => openChangePasswordModal(user?.id ?? null)}>
                            Сменить пароль
                        </Link>
                    </Box>
                </Box>
            </Paper>
        </Box>
    )
}