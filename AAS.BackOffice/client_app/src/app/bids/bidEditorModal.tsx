import { Box, Button, TextField } from '@mui/material';
import React, { useEffect, useState } from 'react';
import { BidBlank } from '../../domain/bids/bidBlank';
import { BidsProvider } from '../../domain/bids/bidProvider';
import { UserBlank } from '../../domain/users/userBlank';
import { UsersProvider } from '../../domain/users/usersProvider';
import { SaveButton } from '../../sharedComponents/buttons/button';
import { InputForm } from '../../sharedComponents/inputs/inputForm';
import { AsyncDialogProps } from '../../sharedComponents/modals/async/types';
import { Modal, ModalActions, ModalBody, ModalTitle } from '../../sharedComponents/modals/modal';

interface Props {
    userId: string | null;
}

export const BidEditorModal: React.FC<AsyncDialogProps<Props, boolean>> = ({ open, handleClose, data: props }) => {
    const [bidBlank, setBidBlank] = useState<BidBlank>(BidBlank.getDefault());
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


    async function saveBidBlank() {
        const result = await BidsProvider.saveBid(bidBlank)

        if (!result.isSuccess) return alert(result.errors[0].message)

        alert('Изменения сохранены');
        handleClose();
    }

    return (
        <Modal isOpen={open} onClose={handleClose}>
            <ModalTitle onClose={handleClose}>
                {props.userId !== null ? "Редактирование" : "Создание"} заявки
            </ModalTitle>
            <ModalBody sx={{ width: 500 }}>
                <Box sx={{
                    display: 'flex',
                    gap: 1.5,
                    flexDirection: 'column'
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
            </ModalBody>
            <ModalActions>
                <SaveButton
                    variant="contained"
                    onClick={() => saveBidBlank()} />
            </ModalActions>
        </Modal>
    )
}