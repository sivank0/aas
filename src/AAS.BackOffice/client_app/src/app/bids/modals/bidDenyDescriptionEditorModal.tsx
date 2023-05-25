import { Box } from '@mui/material';
import React, { useEffect, useState } from 'react';
import { Bid } from '../../../domain/bids/bid';
import { BidsProvider } from '../../../domain/bids/bidProvider';
import { SaveButton } from '../../../sharedComponents/buttons/button';
import { InputForm } from '../../../sharedComponents/inputs/inputForm';
import { AsyncDialogProps } from '../../../sharedComponents/modals/async/types';
import { Modal, ModalActions, ModalBody, ModalTitle } from '../../../sharedComponents/modals/modal';

interface Props {
    bidId: string;
}

export const BidDenyDescriptionEditorModal: React.FC<AsyncDialogProps<Props, boolean>> = ({ open, handleClose, data: props }) => {
    const [bid, setBid] = useState<Bid | null>(null);
    const [bidDenyDesciption, setBidDenyDescription] = useState<string | null>(null);

    useEffect(() => {
        async function init() {
            if (props.bidId === null) return;

            const bid = await BidsProvider.getBidById(props.bidId)

            if (bid === null) {
                handleClose(false);
                return alert('Заявка не найдена');
            }

            setBid(bid);
            setBidDenyDescription(bid.denyDescription);
        }

        if (open) init();

        return () => cleanStates()
    }, [open, props.bidId])

    function cleanStates() {
        setBid(null);
        setBidDenyDescription(null);
    }

    async function changeBidDenyDescription() {
        if (bid === null) return alert("Невозможно заполнить поле 'Причина отказа' в несуществующую заявку");

        const result = await BidsProvider.changeBidDenyDescription(bid.id, bidDenyDesciption);

        if (!result.isSuccess) return alert(result.errors[0].message);

        handleClose(true);
    }

    return (
        <Modal isOpen={open} onClose={() => handleClose(false)}>
            <ModalTitle onClose={() => handleClose(false)}>
                Причина отказа в заявке №{bid?.number}
            </ModalTitle>
            <ModalBody sx={{ width: 500 }}>
                <Box sx={{
                    display: 'flex',
                    gap: 1.5,
                    flexDirection: 'column'
                }}>
                    <InputForm
                        type="text-area"
                        label='Описание'
                        placeholder='Введите описание'
                        minRows={3}
                        value={bidDenyDesciption ?? ''}
                        onChange={(denyDescription) => setBidDenyDescription(denyDescription)} />
                </Box>
            </ModalBody>
            <ModalActions>
                <SaveButton
                    variant="contained"
                    onClick={() => changeBidDenyDescription()} />
            </ModalActions>
        </Modal>
    )
}
