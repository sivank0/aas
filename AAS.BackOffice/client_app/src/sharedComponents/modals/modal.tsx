import { Box, Dialog, DialogActions, DialogContent, DialogTitle, IconButton, SxProps, Theme, Tooltip } from "@mui/material";
import React, { PropsWithChildren } from "react";
import CloseIcon from '@mui/icons-material/Close';

interface ModalProps {
    isOpen: boolean;
    onClose: () => void;
}

export const Modal = (props: PropsWithChildren<ModalProps>) => {
    return (
        <Dialog open={props.isOpen}
            onClose={props.onClose}>
            {props.children}
        </Dialog>
    )
}

interface ModalTitleProps {
    onClose: () => void;
}

export const ModalTitle = (props: PropsWithChildren<ModalTitleProps>) => {
    return (
        <DialogTitle sx={{ position: "relative" }}>
            {props.children}
            <Tooltip title="Закрыть">
                <IconButton sx={{ position: "absolute", right: 4, top: 4 }} onClick={() => props.onClose()}>
                    <CloseIcon />
                </IconButton>
            </Tooltip>
        </DialogTitle>
    )
}

interface ModalBodyProps {
    sx?: SxProps<Theme>
}

export const ModalBody = (props: PropsWithChildren<ModalBodyProps>) => {
    return (
        <DialogContent sx={{ "&.MuiDialogContent-root": { paddingX: 2, paddingY: 1 }, ...props.sx }}>
            {props.children}
        </DialogContent>
    )
}

export const ModalActions = (props: PropsWithChildren) => {
    return (
        <DialogActions>
            {props.children}
        </DialogActions>
    )
}