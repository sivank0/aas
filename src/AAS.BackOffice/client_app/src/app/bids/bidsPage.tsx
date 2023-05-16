import {Box, Button, Container, Divider, Grid, Menu, MenuItem, ToggleButtonGroup, Typography} from '@mui/material';
import React, {useEffect, useState} from 'react';
import useDialog from '../../hooks/useDialog';
import {ConfirmDialogModal} from '../../sharedComponents/modals/modal';
import {BidsProvider} from '../../domain/bids/bidProvider';
import {BidEditorModal} from './bidEditorModal';
import {Bid} from '../../domain/bids/bid';
import {BidCard} from '../../sharedComponents/cards/bidCard';
import {AddButton} from '../../sharedComponents/buttons/button';
import {BrowserType} from '../../tools/browserType';
import {PaginationButtons} from '../../sharedComponents/buttons/paginationButtons';
import {FilterAlt} from "@mui/icons-material";
import {Enum} from "../../tools/types/enum";
import {BidStatus} from "../../domain/bids/bidStatus";
import {ToggleButtons} from "../../sharedComponents/buttons/toggleButtons";
import {BidBlank} from "../../domain/bids/bidBlank";

type PaginationState = {
    page: number;
    countInPage: number;
    totalRows: number | null;
}

export const BidsPage = () => {
    const [bids, setBids] = useState<Bid[]>([]);
    const [paginationState, setPaginationState] = useState<PaginationState>({
        page: 1,
        countInPage: 50,
        totalRows: null
    });
    const [bidBlank, setBidBlank] = useState<BidBlank>(BidBlank.getDefault());

    const bidEditorModal = useDialog(BidEditorModal);

    useEffect(() => {
        init();
    }, [paginationState.page, paginationState.countInPage])

    async function init() {
        const pagedBids = await BidsProvider.getBidPage(paginationState.page, paginationState.countInPage);
        setBids(pagedBids.values);
        setPaginationState(state => ({...state, totalRows: pagedBids.totalRows}))
    }

    async function openBidEditorModal(bidId: string | null = null) {
        const isEdited = await bidEditorModal.show({bidId});

        if (!isEdited) return;

        init();
    }

    const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
    const open = Boolean(anchorEl);
    const handleClick = (event: React.MouseEvent<HTMLButtonElement>) => {
        setAnchorEl(event.currentTarget);
    };
    const handleClose = () => {
        setAnchorEl(null);
    };

    return (
        <Container maxWidth={false}>
            <Box sx={{display: "flex", justifyContent: "space-between"}}>
                <Button variant={"contained"}
                        aria-controls={open ? 'basic-menu' : undefined}
                        aria-haspopup="true"
                        aria-expanded={open ? 'true' : undefined}
                        onClick={handleClick}>
                    <FilterAlt/>
                </Button>
                <Menu
                    anchorEl={anchorEl}
                    open={open}
                    onClose={handleClose}
                    MenuListProps={{
                        'aria-labelledby': 'basic-button',
                    }}
                >
                    <MenuItem>
                        <ToggleButtons
                            value={bidBlank.status}
                            options={Enum.getNumberValues<BidStatus>(BidStatus)}
                            getOptionLabel={(option) => BidStatus.getDisplayName(option)}
                            onChange={(status) => setBidBlank(blank => ({...blank, status}))}/>
                    </MenuItem>
                </Menu>
                <AddButton onClick={() => openBidEditorModal()}>
                    Создать заявку
                </AddButton>
            </Box>
            <Divider sx={{marginY: 3}}/>
            <Grid container spacing={2}>
                {
                    bids.map((bid) =>
                        <Grid key={bid.id} item
                              lg={window.browserType === BrowserType.Desktop ? 4 : undefined}
                              md={window.browserType === BrowserType.Desktop ? 6 : undefined}>
                            <BidCard
                                bid={bid}
                                openBidEditor={(bidId) => openBidEditorModal(bidId)}/>
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
                    countInPageOptions={[50, 100, 150]}
                    onChangePage={(page) => setPaginationState(state => ({...state, page}))}
                    onChangeCountInPage={(countInPage) => setPaginationState(state => ({...state, countInPage}))}/>
            </Box>
        </Container>
    )
}