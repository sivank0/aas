import { Avatar, Tooltip } from '@mui/material';
import React, { useMemo } from 'react';

interface Props{
    imageSrc: string | null | undefined;
    size?: number;
    title: string;
    tooltipTitle?: React.ReactNode;
    disableTooltip?: boolean;
}

export const TooltippedAvatar = (props: Props) =>{
    const stringAvatar = useMemo(() => {
        if(String.isNullOrWhitespace(props.title))return null;

        const titleParts = props.title.split(' ');

        if(titleParts.length === 0) return null;

        if(titleParts.length === 1)
            return titleParts[0][0];

        return `${titleParts[0][0]}${titleParts[1][0]}`;
    }, [props.title])

    return(
        <Tooltip title={props.tooltipTitle} disableHoverListener={props.disableTooltip}>
            <Avatar src={props.imageSrc ?? ""}>
                {stringAvatar}            
            </Avatar>
        </Tooltip>
    )
}