import { Box, Button } from '@mui/material';
import React, { useEffect, useState } from 'react';
import { UserBlank } from '../../domain/users/userBlank';
import { UsersProvider } from '../../domain/users/usersProvider';
import { SaveButton } from '../../sharedComponents/buttons/button';
import { InputForm } from '../../sharedComponents/inputs/inputForm';
import { AsyncDialogProps } from '../../sharedComponents/modals/async/types';
import { Modal, ModalActions, ModalBody, ModalTitle } from '../../sharedComponents/modals/modal';

interface Props {
    userId: string | null;
}

export const UserEditorModal: React.FC<AsyncDialogProps<Props, boolean>> = ({ open, handleClose, data: props }) => {
    const [userBlank, setUserBlank] = useState<UserBlank>(UserBlank.getDefault());

    useEffect(() => {
        async function init() {
            if (props.userId === null) return setUserBlank(UserBlank.getDefault());

            const user = await UsersProvider.getUserById(props.userId);

            if (user === null) return;

            setUserBlank(UserBlank.fromUser(user));
        }

        if (open) init();

        return () => setUserBlank(UserBlank.getDefault());
    }, [props.userId, open])

    async function saveUserBlank() {
        const result = await UsersProvider.saveUser(userBlank)

        if (!result.isSuccess) return alert(result.errors[0].message)

        alert('Изменения сохранены');
        handleClose();
    }

    return (
        <Modal isOpen={open} onClose={handleClose}>
            <ModalTitle onClose={handleClose}>
                {props.userId !== null ? "Редактирование" : "Добавление"} пользователя
            </ModalTitle>
            <ModalBody sx={{ width: 500 }}>
                <Box sx={{
                    display: 'flex',
                    gap: 1.5,
                    flexDirection: 'column'
                }}>
                    <Box sx={{
                        display: 'flex',
                        flexDirection: 'row'
                    }}>
                        <InputForm
                            sx={{ marginRight: 2 }}
                            type="text"
                            label='Имя'
                            placeholder='Введите имя'
                            value={userBlank.firstName}
                            onChange={(firstName) => setUserBlank(blank => ({ ...blank, firstName }))} />
                        <InputForm
                            type="text"
                            label='Фамилия'
                            placeholder='Введите фамилию'
                            value={userBlank.lastName}
                            onChange={(lastName) => setUserBlank(blank => ({ ...blank, lastName }))} />
                    </Box>
                    <InputForm
                        type="text"
                        label='Отчество'
                        placeholder='Введите отчество'
                        value={userBlank.middleName}
                        onChange={(middleName) => setUserBlank(blank => ({ ...blank, middleName }))} />
                    <InputForm
                        type="text"
                        label='Телефон'
                        placeholder='Введите телефон'
                        value={userBlank.phoneNumber}
                        onChange={(phoneNumber) => setUserBlank(blank => ({ ...blank, phoneNumber }))} />
                    <InputForm
                        type="text"
                        label='Email'
                        placeholder='Введите email'
                        value={userBlank.email}
                        onChange={(email) => setUserBlank(blank => ({ ...blank, email }))} />
                    {
                        props.userId === null &&
                        <>
                            <InputForm
                                type="password"
                                label='Пароль'
                                placeholder='Введите пароль'
                                value={userBlank.password}
                                onChange={(password) => setUserBlank(blank => ({ ...blank, password }))} />
                            <InputForm
                                type="password"
                                label='Повтор пароля'
                                placeholder='Повторите пароль'
                                value={userBlank.rePassword}
                                onChange={(rePassword) => setUserBlank(blank => ({ ...blank, rePassword }))} />
                        </>
                    }
                </Box>
            </ModalBody>
            <ModalActions>
                <SaveButton
                    variant="contained"
                    onClick={() => saveUserBlank()} />
            </ModalActions>
        </Modal>
    )
}