import { Box, Dialog, DialogActions, DialogContent, DialogTitle, SxProps, Theme } from "@mui/material";
import React, { PropsWithChildren } from "react";
import { BrowserType } from "../../tools/browserType";
import { AsyncDialogProps } from "./async/types";
import { CancelButton, CloseIconButton, SuccessButton } from "../buttons/button";

interface ModalProps {
    isOpen: boolean;
    onClose: () => void;
}

export const Modal = (props: PropsWithChildren<ModalProps>) => {
    return (
        <Dialog
            fullScreen={window.browserType === BrowserType.Mobile}
            sx={{ overflow: "hidden" }}
            maxWidth={false}
            open={props.isOpen}
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
            <CloseIconButton
                title="Закрыть"
                sx={{ position: "absolute", right: 4, top: 4 }}
                onClick={() => props.onClose()} />
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

interface ConfirmDialogProps {
    title: string;
    body?: React.ReactNode;
}

export const ConfirmDialogModal: React.FC<AsyncDialogProps<ConfirmDialogProps, boolean>> = ({ open, handleClose, data }) => {
    return (
        <Modal isOpen={open} onClose={() => handleClose(false)}>
            <Box sx={window.browserType === BrowserType.Mobile ? {} : { width: 600 }}>
                {
                    data.title !== undefined &&
                    <ModalTitle onClose={() => handleClose(false)}>
                        {data.title}
                    </ModalTitle>
                }
                {
                    data.body !== undefined &&
                    <ModalBody>
                        {data.body}
                    </ModalBody>
                }
                <ModalActions>
                    <SuccessButton onClick={() => handleClose(true)}>Да</SuccessButton>
                    <CancelButton onClick={() => handleClose(false)}>Нет</CancelButton>
                </ModalActions>
            </Box>
        </Modal>
    )
} 