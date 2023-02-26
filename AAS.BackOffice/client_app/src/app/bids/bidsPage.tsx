import { Box, Button, Container, Divider, Grid, Typography } from '@mui/material';
import React, { useEffect, useState } from 'react';
import useDialog from '../../hooks/useDialog';
import { ConfirmDialogModal } from '../../sharedComponents/modals/modal';
import { BidsProvider } from '../../domain/bids/bidProvider';
import { BidEditorModal } from './bidEditorModal';
import { Bid } from '../../domain/bids/bid';
import { BidCard } from '../../sharedComponents/cards/bidCard';
import { AddButton } from '../../sharedComponents/buttons/button';
import { BrowserType } from '../../tools/browserType/browserType';
import { PaginationButtons } from '../../sharedComponents/buttons/paginationButtons';

type PaginationState = {
    page: number;
    countInPage: number;
    totalRows: number | null;
}

export const BidsPage = () => {
    const [bids, setBids] = useState<Bid[]>([]);
    const [paginationState, setPaginationState] = useState<PaginationState>({ page: 1, countInPage: 50, totalRows: null });

    const confirmationDialog = useDialog(ConfirmDialogModal);
    const bidEditorModal = useDialog(BidEditorModal);

    useEffect(() => {
        init();
    }, [paginationState.page, paginationState.countInPage])

    async function init() {
        const pagedBids = await BidsProvider.getBidPage(paginationState.page, paginationState.countInPage);
        setBids(pagedBids.values);
        setPaginationState(state => ({ ...state, totalRows: pagedBids.totalRows }))
    }

    async function openBidEditorModal(bidId: string | null = null) {
        const isEdited = await bidEditorModal.show({ bidId });

        if (!isEdited) return;

        init();
    }

    async function removeBid(bidId: string) {
        const removingBid = bids.find(bids => bids.id === bidId) ?? null;

        const isConfirmed = await confirmationDialog.show({ title: `Вы действительно хотите удалить заявку: ${removingBid?.title}` })

        if (!isConfirmed) return;

        const result = await BidsProvider.removeBid(bidId);

        if (!result.isSuccess) return;
    }

    return (
        <Container maxWidth={false}>
            <Box sx={{ display: "flex", justifyContent: "space-between" }}>
                <Typography variant="h5">Заявки</Typography>
                <AddButton onClick={() => openBidEditorModal()}>
                    Создать заявку
                </AddButton>
            </Box>
            <Divider sx={{ marginY: 3 }} />
            <Grid container spacing={2}>
                {
                    bids.map((bid) =>
                        <Grid key={bid.id} item
                            lg={window.browserType === BrowserType.Desktop ? 4 : undefined}
                            md={window.browserType === BrowserType.Desktop ? 6 : undefined}>
                            <BidCard
                                bid={bid}
                                openBidEditor={(bidId) => openBidEditorModal(bidId)} />
                        </Grid>
                    )
                }
            </Grid>
            <Box sx={{
                marginTop: '15px',
                display: 'flex',
                width: '100%',
                justifyContent: 'right'
            }}>
                <PaginationButtons
                    page={paginationState.page}
                    countInPage={paginationState.countInPage}
                    onChangePage={(page) => setPaginationState(state => ({ ...state, page }))}
                    onChangeCountInPage={(countInPage) => setPaginationState(state => ({ ...state, countInPage }))} />
            </Box>
        </Container >
    )
}