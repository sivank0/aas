import { Button, Typography } from '@mui/material';
import React, { useState } from 'react';
import { EmailVerificationsProvider } from '../../../domain/emailVerifications/emailVerificationsProvider';
import { InputForm } from '../../../sharedComponents/inputs/inputForm';
import { AsyncDialogProps } from '../../../sharedComponents/modals/async/types';
import { Modal, ModalBody, ModalTitle } from '../../../sharedComponents/modals/modal';

interface Props { }

export const CanNotLogInHelpModal: React.FC<AsyncDialogProps<Props, boolean>> = ({ open, handleClose, data: props }) => {
    const [userEmail, setUserEmail] = useState<string | null>(null);

    async function resendEmailVerificationMessage() {
        const result = await EmailVerificationsProvider.resendEmailVerificationMessage(userEmail);

        if (!result.isSuccess) return alert(result.errors[0].message);

        alert("Мы выслали ещё одно письмо с подтверждением почты");
    }

    return (
        <Modal isOpen={open} onClose={() => handleClose(false)}>
            <ModalTitle onClose={() => handleClose(false)}>
                Не удается войти в аккаунт
            </ModalTitle>
            <ModalBody
                sx={{
                    width: 500,
                    display: 'flex',
                    flexDirection: 'column',
                    gap: 2
                }}>
                <Typography>
                    Если вы не можете найти письмо о подтверждении своего Email адреса, попробуйте посмотреть в папке "Спам"
                    <br />
                    Если же письма нет в папке "Спам", то заполните форму ниже
                </Typography>
                <InputForm
                    label='Адрес электронной почты'
                    placeholder='Введите адрес электронной почты'
                    type='text'
                    value={userEmail}
                    onChange={(email) => setUserEmail(email)} />
                <Button onClick={() => resendEmailVerificationMessage()}>
                    Отправить письмо ещё раз
                </Button>
            </ModalBody>
        </Modal>
    )
}
