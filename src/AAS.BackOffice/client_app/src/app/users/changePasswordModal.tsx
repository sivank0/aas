import { Box, Typography } from '@mui/material';
import React, { useEffect, useState } from 'react';
import { User } from '../../domain/users/user';
import { UsersProvider } from '../../domain/users/usersProvider';
import { SaveButton } from '../../sharedComponents/buttons/button';
import { InputForm } from '../../sharedComponents/inputs/inputForm';
import { AsyncDialogProps } from '../../sharedComponents/modals/async/types';
import { Modal, ModalActions, ModalBody, ModalTitle } from '../../sharedComponents/modals/modal';

interface Props {
    userId: string | null;
}

export const ChangePasswordModal: React.FC<AsyncDialogProps<Props, boolean>> = ({ open, handleClose, data: props }) => {
    const [user, setUser] = useState<User | null>(null);
    const [password, setPassword] = useState<string | null>(null);
    const [rePassword, setRePassword] = useState<string | null>(null);

    useEffect(() => {
        async function init() {
            if (props.userId === null) return;

            const user = await UsersProvider.getUserById(props.userId);

            if (user === null) {
                handleClose();
                return alert("Пользователь не найден");
            }

            setUser(user);
        }

        if (open) init();

        function cleanUpState() {
            setUser(null);
            setPassword(null);
            setRePassword(null);
        }

        return () => cleanUpState()
    }, [open])

    async function changeUserPassword() {
        if (props.userId === null) return;

        const result = await UsersProvider.changeUserPassword(props.userId, password, rePassword);

        if (!result.isSuccess) return alert(result.errors[0].message)

        alert('Изменения сохранены')
        handleClose();
    }

    return (
        <Modal isOpen={open} onClose={handleClose}>
            <ModalTitle onClose={handleClose}>
                Изменение пароля пользователя
            </ModalTitle>
            <Typography sx={{
                display: 'flex',
                paddingLeft: '17px'
            }}>
                {user?.fullName}
            </Typography>
            <ModalBody sx={{ width: 500 }}>
                <Box sx={{
                    display: 'flex',
                    gap: 1.5,
                    flexDirection: 'column'
                }}>
                    <InputForm
                        type="password"
                        label='Пароль'
                        placeholder='Введите пароль'
                        value={password}
                        onChange={(password) => setPassword(password)} />
                    <InputForm
                        type="password"
                        label='Повтор пароля'
                        placeholder='Повторите пароль'
                        value={rePassword}
                        onChange={(rePassword) => setRePassword(rePassword)} />
                </Box>
            </ModalBody>
            <ModalActions>
                <SaveButton
                    variant="contained"
                    onClick={() => changeUserPassword()} />
            </ModalActions>
        </Modal>
    )
}