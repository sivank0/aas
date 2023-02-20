import { Box, Button, Card, CardContent, Container, Divider, IconButton, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Tooltip, Typography } from '@mui/material';
import React, { useEffect, useState } from 'react';
import { User } from '../../domain/users/user';
import { UsersProvider } from '../../domain/users/usersProvider';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import AddIcon from '@mui/icons-material/Add';
import useDialog from '../../hooks/useDialog';
import { ConfirmDialogModal } from '../../sharedComponents/modals/modal';
import { Password, SignalCellularNullRounded } from '@mui/icons-material';
import { BidBlank } from '../../domain/bids/bidBlank';
import { BidsProvider } from '../../domain/bids/bidProvider';
import { BidEditorModal } from './bidEditorModal';
import { Bid } from '../../domain/bids/bid';

export const BidsPage = () => {
    const [bids, setBids] = useState<Bid[]>([]);

    const confirmationDialog = useDialog(ConfirmDialogModal);
    const bidEditorModal = useDialog(BidEditorModal);


    useEffect(() => {
        async function init() {
            const bids = await BidsProvider.getBids();
            setBids(bids);
        }
        init();
    }, [])

    function openBidEditorModal(bidId: string | null = null) {
        bidEditorModal.show({ userId: bidId });
    }

    async function removeBid(bidId: string) {
        const removingBid = bids.find(bids => bids.id === bidId) ?? null;

        const isConfirmed = await confirmationDialog.show({ title: `Вы действительно хотите удалить заявку: ${removingBid?.title}` })

        if (!isConfirmed) return;

        const result = await BidsProvider.removeBid(bidId);

        if (!result.isSuccess) return;
    }

    return (
        <Container maxWidth={false}
            sx={{ display: 'flex' }}>
            <Button startIcon={<AddIcon />} variant="outlined" onClick={() => openBidEditorModal()}>
                Создать заявку
            </Button>
            {bids.map(bid =>
                <Card>
                    <CardContent>
                        <Typography sx={{ fontSize: 14 }} >
                            {bid.title}
                        </Typography>
                        <IconButton onClick={() => removeBid(bid.id!)}>
                            <DeleteIcon />
                        </IconButton>
                    </CardContent>
                </Card>
            )}
        </Container>
    )
}