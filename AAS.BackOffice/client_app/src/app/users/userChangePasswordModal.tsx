import { Box, Button } from '@mui/material';
import React, { useEffect, useState } from 'react';
import { UserBlank } from '../../domain/users/userBlank';
import { UsersProvider } from '../../domain/users/usersProvider';
import { InputForm } from '../../sharedComponents/inputs/inputForm';
import { Modal, ModalActions, ModalBody, ModalTitle } from '../../sharedComponents/modals/modal';

interface Props {
    userId: string | null;
    isOpen: boolean;
    onClose: () => void;
}

export const UserChangePasswordModal = (props: Props) => {
    const [userBlank, setUserBlank] = useState<UserBlank>(UserBlank.getDefault());

    useEffect(() => {
        async function init() {
            if (props.userId === null) return setUserBlank(UserBlank.getDefault());

            const user = await UsersProvider.getUserById(props.userId);

            if (user === null) return;

            setUserBlank(UserBlank.fromUser(user));
        }
    }, [props.userId])

    async function saveChangedUser() {
        const result = await UsersProvider.saveUser(userBlank)
        if (!result.isSuccess) return alert(result.errors[0].message)
        else alert('Изменения сохранены')
    }

    return (
        <Modal isOpen={props.isOpen} onClose={props.onClose}>
            <ModalTitle onClose={props.onClose}>
                Изменение пароля
            </ModalTitle>
            <ModalBody sx={{ width: 500 }}>
                <Box sx={{
                    display: 'flex',
                    gap: 1.5,
                    flexDirection: 'column'
                }}>
                    <InputForm
                        type="text"
                        label='Пароль'
                        placeholder='Введите пароль'
                        value={userBlank.password}
                        onChange={(password) => setUserBlank(blank => ({ ...blank, password }))} />
                    <InputForm
                        type="text"
                        label='Повтор пароля'
                        placeholder='Повторите пароль'
                        value={userBlank.rePassword}
                        onChange={(rePassword) => setUserBlank(blank => ({ ...blank, rePassword }))} />
                </Box>
            </ModalBody>
            <ModalActions>
                <Button variant='contained' onClick={() => saveChangedUser()}>
                    Сохранить
                </Button>
            </ModalActions>
        </Modal>
    )
}