import { DragDropContext, Draggable, DropResult, Droppable } from "@hello-pangea/dnd";
import {
    Box,
    Container,
    Divider,
    Paper
} from '@mui/material';
import React, {useEffect, useMemo, useState} from 'react';
import { Bid } from '../../domain/bids/bid';
import { BidsProvider } from '../../domain/bids/bidProvider';
import { BidStatus } from "../../domain/bids/bidStatus";
import useDialog from "../../hooks/useDialog";
import { AddButton } from '../../sharedComponents/buttons/button';
import { BidCard } from '../../sharedComponents/cards/bidCard';
import { Enum } from "../../tools/types/enum";
import { BidDenyDescriptionEditorModal } from "./modals/bidDenyDescriptionEditorModal";
import { BidEditorModal } from "./modals/bidEditorModal";
import {ThemeMode} from "../../sharedComponents/themeMode";

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
const themeModeKey = 'themeMode'

export const BidsPage = () => {
    const themeMode = useMemo(()=>{
        const localThemeMode = localStorage.getItem(themeModeKey)

        if (String.isNullOrWhitespace(localThemeMode)) {
            return localStorage.setItem(themeModeKey, ThemeMode.getValue(ThemeMode.Light))
        }

       return localThemeMode === ThemeMode.getValue(ThemeMode.Light)
            ? ThemeMode.Light
            : ThemeMode.Dark
    },[localStorage.getItem(themeModeKey)])

    const bidDenyDescriptionEditorModal = useDialog(BidDenyDescriptionEditorModal);
    const bidEditorModal = useDialog(BidEditorModal);

    const [columns, setColumns] = useState<Column[]>([]);
    const [bids, setBids] = useState<Bid[]>([]);
    const [isInit, setIsInit] = useState<boolean>(false);

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

    async function onDragEnd(result: DropResult) {
        if (!result.destination) return;

        const { source, destination, draggableId } = result;

        if (source.droppableId === destination.droppableId) return;

        const sourceColumn = columns.find(col => col.id == source.droppableId);
        const destColumn = columns.find(col => col.id == destination.droppableId);

        if (sourceColumn === null || destColumn === null) return;

        const sourceItems = sourceColumn!.bids;
        const destItems = destColumn!.bids;
        const [removed] = sourceItems.splice(source.index, 1);
        destItems.splice(destination.index, 0, removed);

        const destColumnStatus = destColumn!.status;

        await changeBidStatus(draggableId, destColumnStatus);
    };

    async function changeBidStatus(bidId: string, bidStatus: BidStatus) {
        if (bidStatus === BidStatus.Denied) {
            const isEdited = await bidDenyDescriptionEditorModal.show({ bidId });

            if (!isEdited) {
                loadBids();
                return alert(`Для того, чтобы переместить заявку в статус ${BidStatus.getDisplayName(bidStatus)} необходимо ввести причину отказа`);
            }
        }

        const result = await BidsProvider.changeBidStatus(bidId, bidStatus);

        if (!result.isSuccess) alert(result.errors[0].message);

        loadBids();
    }

    async function openBidEditorModal(bidId: string | null = null) {
        const isEdited = await bidEditorModal.show({ bidId });

        if (!isEdited) return;

        loadBids();
    }

    return (
        <Container maxWidth={false} sx={{ paddingTop: 2, height: '100%' }}>
            <Box sx={{ display: "flex", justifyContent: "space-between" }}>
                <AddButton onClick={() => openBidEditorModal()}>
                    Создать заявку
                </AddButton>
            </Box>
            <Divider sx={{ marginY: 3 }} />
            <DragDropContext onDragEnd={(result) => onDragEnd(result)}>
                <Box sx={{
                    display: 'grid',
                    overflowY: 'hidden',
                    gridTemplateColumns: `repeat(${columns.length}, minmax(340px, 1fr))`,
                    height: 'calc(100% - 110px)',
                    overflowX: 'auto',
                    gap: 3
                }}>
                    {
                        columns.map(column =>
                            <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2 }}>
                                <Paper
                                    key={`head--${column.id}`}
                                    sx={{
                                        padding: 1,
                                        textAlign: 'center',
                                        backgroundColor: theme => themeMode === ThemeMode.Light
                                            ? theme.palette.grey[300]
                                            : theme.palette.grey[700],
                                    }}>
                                    {column.title}
                                </Paper>
                                <Droppable droppableId={column.id} key={column.id}>
                                    {
                                        (provided, _) => {
                                            return (
                                                <Box
                                                    {...provided.droppableProps}
                                                    ref={provided.innerRef}
                                                    sx={{
                                                        height: '100%',
                                                        border: '1px dashed',
                                                        borderColor: theme => themeMode === ThemeMode.Light
                                                            ? theme.palette.grey[700]
                                                            : theme.palette.grey[300],
                                                        borderRadius: 1,
                                                        padding: 1
                                                    }}>
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
                                                                                openBidEditor={(bidId) => openBidEditorModal(bidId)}
                                                                                sx={{
                                                                                    marginBottom: 2,
                                                                                    ':last-child': {
                                                                                        marginBottom: 0
                                                                                    }
                                                                                }} />
                                                                        )
                                                                    }
                                                                }
                                                            </Draggable>
                                                        )
                                                    }
                                                    {provided.placeholder}
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