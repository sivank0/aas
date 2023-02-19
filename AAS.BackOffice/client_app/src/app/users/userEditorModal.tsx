import { Button } from '@mui/material';
import React, { useEffect, useState } from 'react';
import { UserBlank } from '../../domain/users/userBlank';
import { UsersProvider } from '../../domain/users/usersProvider';
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
    }, [props.userId])

    return (
        <Modal isOpen={open} onClose={handleClose}>
            <ModalTitle onClose={handleClose}>
                {props.userId !== null ? "Редактирование" : "Добавление"} пользователя
            </ModalTitle>
            <ModalBody sx={{ width: 500 }}>
                <InputForm
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
                <InputForm
                    type="text"
                    label='Отчество'
                    placeholder='Введите отчество'
                    value={userBlank.middleName}
                    onChange={(middleName) => setUserBlank(blank => ({ ...blank, middleName }))} />
            </ModalBody>
            <ModalActions>
                <Button variant='contained'>
                    Сохранить
                </Button>
            </ModalActions>
        </Modal>
    )
}