import { Box, Button, ButtonGroup, TextField, ToggleButton, ToggleButtonGroup } from '@mui/material';
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
    bidId: string | null;
}

export const BidEditorModal: React.FC<AsyncDialogProps<Props, boolean>> = ({ open, handleClose, data: props }) => {
    const [bidBlank, setBidBlank] = useState<BidBlank>(BidBlank.getDefault());

    useEffect(() => {
        async function init() {
            if (props.bidId === null) return setBidBlank(BidBlank.getDefault());

            const bid = await BidsProvider.getBidById(props.bidId);

            if (bid === null) return;

            setBidBlank(BidBlank.fromBid(bid));
        }

        if (open) init();

        return () => setBidBlank(BidBlank.getDefault());
    }, [props.bidId, open])


    async function saveBidBlank() {
        const result = await BidsProvider.saveBid(bidBlank)

        if (!result.isSuccess) return alert(result.errors[0].message)

        alert('Изменения сохранены');
        handleClose();
    }

    function onChange(value: number) {
        if (!value) return
        bidBlank.status = value
    }

    return (
        <Modal isOpen={open} onClose={handleClose}>
            <ModalTitle onClose={handleClose}>
                {props.bidId !== null ? "Редактирование" : "Создание"} заявки
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
                        label='Тема'
                        placeholder='Введите тему'
                        value={bidBlank.title}
                        onChange={(title) => setBidBlank(blank => ({ ...blank, title }))} />
                    <InputForm
                        type="text"
                        label='Описание'
                        placeholder='Введите описание'
                        value={bidBlank.description}
                        onChange={(description) => setBidBlank(blank => ({ ...blank, description }))} />
                    <ToggleButtonGroup sx={{ paddingX: 0 }}
                        value={bidBlank.status}
                        onClick={() => console.log(bidBlank.status)}
                        onChange={(_, value: number) => onChange(value)} // Не воркает анимейшн
                        exclusive>
                        <ToggleButton value={1} color='warning' sx={{ paddingX: 1 }}>
                            Ожидает подтверждения
                        </ToggleButton>
                        <ToggleButton value={2} sx={{ paddingX: 1 }}>
                            Отменено
                        </ToggleButton>
                        <ToggleButton value={3} sx={{ paddingX: 1 }}>
                            В работе
                        </ToggleButton>
                        <ToggleButton value={4} color='success' sx={{ paddingX: 1 }}>
                            Завершено
                        </ToggleButton>
                    </ToggleButtonGroup>
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