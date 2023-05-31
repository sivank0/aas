import AddIcon from '@mui/icons-material/Add';
import CancelIcon from '@mui/icons-material/Cancel';
import CloseIcon from '@mui/icons-material/Close';
import DoneIcon from '@mui/icons-material/Done';
import SaveIcon from '@mui/icons-material/Save';
import { Button, IconButton, SxProps, Theme, Tooltip } from '@mui/material';
import React, { PropsWithChildren } from 'react';

interface ButtonProps {
    variant?: "text" | "outlined" | "contained";
    color?: "primary" | "inherit" | "secondary" | "success" | "error" | "info" | "warning";
    size?: "small" | "medium" | "large";
    sx?: SxProps<Theme>;
    disabled?: boolean;
    onClick?: React.MouseEventHandler<HTMLButtonElement | HTMLAnchorElement>;
}

export const SaveButton = (props: PropsWithChildren<ButtonProps>) => {
    return (
        <Button
            disabled={props.disabled}
            variant={props.variant ?? "contained"}
            sx={props.sx}
            size={props.size}
            color={props.color ?? "primary"}
            startIcon={
                <SaveIcon
                    fontSize={props.size}
                    color={props.color ?? "inherit"} />
            }
            onClick={props.onClick}>
            {props.children ?? "Сохранить"}
        </Button>
    )
}

export const SuccessButton = (props: PropsWithChildren<ButtonProps>) => {
    return (
        <Button
            disabled={props.disabled}
            variant={props.variant ?? "contained"}
            sx={props.sx}
            size={props.size}
            color={props.color ?? "primary"}
            startIcon={
                <DoneIcon
                    fontSize={props.size}
                    sx={{ color: '#fff' }} />
            }
            onClick={props.onClick}>
            {props.children ?? "Подтвердить"}
        </Button>
    )
}

export const CancelButton = (props: PropsWithChildren<ButtonProps>) => {
    return (
        <Button
            disabled={props.disabled}
            variant={props.variant ?? "contained"}
            sx={props.sx}
            size={props.size}
            color={props.color ?? "error"}
            startIcon={
                <CancelIcon
                    fontSize={props.size}
                    sx={{ color: '#fff' }} />
            }
            onClick={props.onClick}>
            {props.children ?? "Отменить"}
        </Button>
    )
}

export const AddButton = (props: PropsWithChildren<ButtonProps>) => {
    return (
        <Button
            disabled={props.disabled}
            variant={props.variant ?? "contained"}
            sx={props.sx}
            size={props.size}
            color={props.color ?? "primary"}
            startIcon={
                <AddIcon
                    fontSize={props.size}
                    color={props.color ?? "inherit"} />
            }
            onClick={props.onClick}>
            {props.children ?? "Сохранить"}
        </Button>
    )
}


interface IconButtonProps extends ButtonProps {
    title: string;
    className?: string;
}

export const CloseIconButton = (props: IconButtonProps) => {
    return (
        <Tooltip title={props.title}>
            <IconButton
                disabled={props.disabled}
                sx={props.sx}
                className={props.className}
                size={props.size}
                onClick={props.onClick}>
                <CloseIcon
                    fontSize={props.size}
                    color={props.color ?? "primary"} />
            </IconButton>
        </Tooltip>
    )
}  