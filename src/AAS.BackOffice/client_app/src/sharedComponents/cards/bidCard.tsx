import { DraggableProvided, DraggableStateSnapshot } from "@hello-pangea/dnd";
import { Box, Card, CardActions, CardContent, CardHeader, SxProps, Theme, Typography } from '@mui/material';
import React from 'react';
import { Bid } from '../../domain/bids/bid';
import { BidStatus } from '../../domain/bids/bidStatus';

interface Props {
    provided: DraggableProvided;
    snapshot: DraggableStateSnapshot;
    bid: Bid;
    sx?: SxProps<Theme>;
    openBidEditor: (bidId: string) => void;
}

export const BidCard = (props: Props) => {

    return (
        <Card
            {...props.provided?.draggableProps}
            {...props.provided?.dragHandleProps}
            ref={props.provided?.innerRef}
            elevation={props.snapshot?.isDragging ? 6 : 3}
            sx={{ ...props.sx, cursor: "pointer" }}
            onClick={() => props.openBidEditor(props.bid.id)}>
            <CardHeader
                title={
                    <Box sx={{ display: "flex", alignItems: "center", gap: 1 }}>
                        <Typography sx={{ fontSize: 14 }}>№{props.bid.number} - {props.bid.title}</Typography>
                    </Box>
                }
                sx={{ paddingBottom: 0 }} />
            <CardContent sx={{ paddingY: 1 }}>
                {
                    !String.isNullOrWhitespace(props.bid.description) &&
                    <Typography sx={{ fontSize: 14 }}>
                        {props.bid.description}
                    </Typography>
                }
            </CardContent>
            <CardActions sx={{ display: "flex", paddingTop: 0, paddingBottom: 1, paddingX: 2 }}>
                {
                    (props.bid.status === BidStatus.Denied) &&
                    <Typography sx={{ display: "flex", marginLeft: '15px', fontSize: 14 }}>
                        {props.bid.denyDescription}
                    </Typography>
                }
            </CardActions>
        </Card>
    )
}