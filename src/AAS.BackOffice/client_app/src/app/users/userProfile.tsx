import EditIcon from '@mui/icons-material/Edit';
import {
    Box,
    Container,
    Divider,
    Fade,
    IconButton,
    Link,
    Paper,
    Typography
} from '@mui/material';
import React, { useEffect, useState } from 'react';
import SystemUser from '../../domain/systemUser';
import { User } from '../../domain/users/user';
import { UserProfileProvider } from '../../domain/users/usersProvider';
import useDialog from '../../hooks/useDialog';
import { ChangePasswordModal } from './changePasswordModal';
import { UserEditorModal } from './userEditorModal';
import {TooltippedAvatar} from "../../sharedComponents/avatar";

export const UserProfile = () => {
    const [user, setUser] = useState<User | null>(null);
    const [isInit, setIsInit] = useState<boolean>(false);

    const userEditorModal = useDialog(UserEditorModal);
    const changePasswordModal = useDialog(ChangePasswordModal);

    useEffect(() => {
        async function init() {
            const user = await UserProfileProvider.getUserProfileById(SystemUser.id);

            if (user === null) return;

            setUser(user);
            setIsInit(true);
        }

        init();
    }, [])

    function openUserEditorModal(userId: string | null) {
        if (userId === null) return;
        userEditorModal.show({ userId });
    }

    function openChangePasswordModal(userId: string | null) {
        if (userId === null) return;
        changePasswordModal.show({ userId });
    }

    return (
        <Container maxWidth={false} sx={{ paddingTop: 2 }}>
            <Box sx={{
                display: 'flex',
                justifyContent: 'center',
                width: '100%',
                minHeight: '500px'
            }}>
                <Fade in={isInit}>
                    <Paper elevation={3}
                        sx={{ width: '560px' }}>
                        <Box sx={{
                            padding: '10px'
                        }}>
                            <Box sx={{
                                display: 'flex',
                                justifyContent: 'space-between',
                                flexDirection: 'row'
                            }}>
                                <TooltippedAvatar imageSrc={SystemUser.photo?.url} title={SystemUser.fullName} />
                                <IconButton onClick={() => openUserEditorModal(user?.id ?? null)}>
                                    <EditIcon />
                                </IconButton>
                            </Box>
                            <Divider sx={{ marginY: 1 }} />
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
                                <Divider sx={{ marginY: 1 }} />

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
                </Fade>
            </Box>
        </Container>
    )
}