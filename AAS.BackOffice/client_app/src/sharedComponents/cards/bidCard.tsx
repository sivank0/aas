import { Box, Card, CardActions, CardContent, CardHeader, Divider, Tooltip, Typography } from '@mui/material';
import React, { useState } from 'react';
import { Bid } from '../../domain/bids/bid';
import { BidStatus } from '../../domain/bids/bidStatus';
import FiberNewIcon from '@mui/icons-material/FiberNew';
import HourglassBottomIcon from '@mui/icons-material/HourglassBottom';
import DoNotDisturbIcon from '@mui/icons-material/DoNotDisturb';
import EngineeringIcon from '@mui/icons-material/Engineering';
import EventAvailableIcon from '@mui/icons-material/EventAvailable';

interface Props {
    bid: Bid;
    openBidEditor: (bidId: string) => void;
}

export const BidCard = (props: Props) => {

    function getBidStatusIcon(status: BidStatus) {
        switch (status) {
            case BidStatus.AwaitingVerification: return <HourglassBottomIcon fontSize='small' />
            case BidStatus.Denied: return <DoNotDisturbIcon fontSize='small' />
            case BidStatus.InWork: return <EngineeringIcon fontSize='small' />
            case BidStatus.Completed: return <EventAvailableIcon fontSize='small' />
        }
    }

    return (
        <Card
            elevation={3}
            sx={{ cursor: "pointer" }}
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
                    <Box sx={{ display: "flex", flexDirection: "column", gap: 0.5 }}>
                        <Divider />
                        <Typography sx={{ fontSize: 14 }}>
                            {props.bid.description}
                        </Typography>
                        <Divider />
                    </Box>
                }
            </CardContent>
            <CardActions sx={{ display: "flex", paddingTop: 0, paddingBottom: 1, paddingX: 2 }}>
                {getBidStatusIcon(props.bid.status)}
                {
                    (props.bid.status === BidStatus.Denied) &&
                    <Typography sx={{ display: "flex", marginLeft: '15px', fontSize: 14 }}>
                        {props.bid.denyDescription}
                    </Typography>
                }
                <Box sx={{
                    display: 'flex',
                    width: '100%',
                    justifyContent: 'right'
                }}>
                    <Typography >
                        {BidStatus.getDisplayName(props.bid.status)}
                    </Typography>
                </Box>
            </CardActions>
        </Card >
    )
}