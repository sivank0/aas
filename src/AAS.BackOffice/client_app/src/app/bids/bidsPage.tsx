import {
    Box,
    Button,
    Container,
    Divider,
    Grid,
    Menu,
    MenuItem,
    ToggleButtonGroup,
    Typography,
    Paper
} from '@mui/material';
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
import {DragDropContext, Draggable, Droppable, DropResult} from "@hello-pangea/dnd";

type PaginationState = {
    page: number;
    countInPage: number;
    totalRows: number | null;
}

type Column = {
    id: string;
    title: string;
    status: BidStatus;
    bids: Bid[];
}

export const BidsPage = () => {

    const [columns, setColumns] = useState<Column[]>([]);
    const [bids, setBids] = useState<Bid[]>([]);
    const [isInit, setIsInit] = useState<boolean>(false);


    const bidEditorModal = useDialog(BidEditorModal);

    useEffect(() => {
        try {
            loadBids();
        } finally {
            setIsInit(true);
        }
    }, [])

    useEffect(() => {
        fillColumns();
    }, [bids])

    function fillColumns() {
        const columns: Column[] = []
        Enum.getNumberValues<BidStatus>(BidStatus).map((bidStatus, index) => {
            columns.push({
                id: `columns--${index}`,
                title: BidStatus.getDisplayName(bidStatus),
                status: bidStatus,
                bids: bids.filter(bid => bid.status === bidStatus)
            })
        })
        setColumns(columns)
    }

    async function loadBids() {
        const bids = await BidsProvider.getBidsAll();
        setBids(bids);
    }

    async function openBidEditorModal(bidId: string | null = null) {
        const isEdited = await bidEditorModal.show({bidId});

        if (!isEdited) return;

        loadBids();
    }

    function onDragEnd(result: DropResult) {
        if (!result.destination) return;

        const {source, destination, draggableId} = result;

        const sourceColumn = columns.find(col => col.id == source.droppableId);
        const destColumn = columns.find(col => col.id == destination.droppableId);

        if (sourceColumn === null || destColumn === null) return;

        const sourceItems = sourceColumn!.bids;
        const destItems = destColumn!.bids;
        const [removed] = sourceItems.splice(source.index, 1);
        destItems.splice(destination.index, 0, removed);

        let isReplaced: boolean = false;

        if (source.droppableId === destination.droppableId) {
            if (source.index !== destination.index) isReplaced = true;
        } else {
            isReplaced = true;
        }

        const destColumnId = destColumn!.id;

    };


    return (
        <Container maxWidth={false}>
            <Box sx={{display: "flex", justifyContent: "space-between"}}>
                <AddButton onClick={() => openBidEditorModal()}>
                    Создать заявку
                </AddButton>
            </Box>
            <Divider sx={{marginY: 3}}/>
            <DragDropContext onDragEnd={(result) => onDragEnd(result)}>
                <Box sx={{
                    display: 'grid',
                    overflowY: 'hidden',
                    gridTemplateColumns: `repeat(${columns.length}, minmax(340px, 1fr))`,
                    overflowX: 'auto',
                    gap: 3
                }}>
                    {
                        columns.map(column =>
                            <Box>
                                <Paper key={`head--${column.id}`}>
                                    {
                                        column.title
                                    }
                                </Paper>
                                <Droppable droppableId={column.id} key={column.id}>
                                    {
                                        (provided, _) => {
                                            return (
                                                <Box {...provided.droppableProps} ref={provided.innerRef}>
                                                    {
                                                        column.bids.map((bid, index) =>
                                                            <Draggable draggableId={bid.id} index={index} key={bid.id}>
                                                                {
                                                                    (provided, snapshot) => {
                                                                        return (
                                                                            <BidCard
                                                                                bid={bid}
                                                                                provided={provided}
                                                                                snapshot={snapshot}
                                                                                openBidEditor={(bidId) => openBidEditorModal(bidId)}/>
                                                                        )
                                                                    }
                                                                }
                                                            </Draggable>
                                                        )
                                                    }
                                                    {
                                                        provided.placeholder
                                                    }
                                                </Box>
                                            )
                                        }
                                    }

                                </Droppable>
                            </Box>
                        )
                    }
                </Box>
            </DragDropContext>
        </Container>
    )
}