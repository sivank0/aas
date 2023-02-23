import { Box, } from '@mui/material';
import React, { useEffect, useState } from 'react';
import { BidBlank } from '../../domain/bids/bidBlank';
import { BidsProvider } from '../../domain/bids/bidProvider';
import { BidStatus } from '../../domain/bids/bidStatus';
import { SaveButton } from '../../sharedComponents/buttons/button';
import { ToggleButtons } from '../../sharedComponents/buttons/toggleButtons';
import { InputForm } from '../../sharedComponents/inputs/inputForm';
import { AsyncDialogProps } from '../../sharedComponents/modals/async/types';
import { Modal, ModalActions, ModalBody, ModalTitle } from '../../sharedComponents/modals/modal';
import { Enum } from '../../tools/enum';

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
        handleClose(true);
    }

    return (
        <Modal isOpen={open} onClose={() => handleClose(false)}>
            <ModalTitle onClose={() => handleClose(false)}>
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
                        type="text-area"
                        label='Описание'
                        placeholder='Введите описание'
                        minRows={3}
                        value={bidBlank.description}
                        onChange={(description) => setBidBlank(blank => ({ ...blank, description }))} />
                    {
                        (props.bidId !== null && bidBlank.status === BidStatus.Denied) &&
                        <InputForm
                            type="text-area"
                            label='Причина отклонения'
                            placeholder='Введите причину отклонения'
                            minRows={3}
                            value={bidBlank.denyDescription}
                            onChange={(denyDescription) => setBidBlank(blank => ({ ...blank, denyDescription }))} />
                    }
                    {
                        props.bidId !== null &&
                        <ToggleButtons
                            value={bidBlank.status}
                            options={Enum.getNumberValues<BidStatus>(BidStatus)}
                            getOptionLabel={(option) => BidStatus.getDisplayName(option)}
                            onChange={(status) => setBidBlank(blank => ({ ...blank, status }))} />
                    }
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