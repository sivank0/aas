import DeleteIcon from '@mui/icons-material/Delete';
import { Box, IconButton, Tooltip } from '@mui/material';
import { endOfDay } from "date-fns";
import { ru } from 'date-fns/locale';
import React, { useEffect, useMemo, useState } from 'react';
import { DayPicker } from 'react-day-picker';
import 'react-day-picker/dist/style.css';
import { AccessPolicy } from '../../../domain/accessPolicies/accessPolicy';
import { BidBlank } from '../../../domain/bids/bidBlank';
import { BidsProvider } from '../../../domain/bids/bidProvider';
import { BidStatus } from '../../../domain/bids/bidStatus';
import { FileArea } from '../../../domain/files/enums/fileArea';
import { FileState } from '../../../domain/files/enums/fileState';
import { FileBlank } from '../../../domain/files/fileBlank';
import SystemUser from '../../../domain/systemUser';
import useDialog from '../../../hooks/useDialog';
import { SaveButton } from '../../../sharedComponents/buttons/button';
import { ToggleButtons } from '../../../sharedComponents/buttons/toggleButtons';
import { InputForm } from '../../../sharedComponents/inputs/inputForm';
import { AsyncDialogProps } from '../../../sharedComponents/modals/async/types';
import { ConfirmDialogModal, Modal, ModalActions, ModalBody, ModalTitle } from '../../../sharedComponents/modals/modal';
import { Enum } from '../../../tools/types/enum';

interface Props {
    bidId: string | null;
}

export const BidEditorModal: React.FC<AsyncDialogProps<Props, boolean>> = ({ open, handleClose, data: props }) => {
    const [bidBlank, setBidBlank] = useState<BidBlank>(BidBlank.getDefault(SystemUser.id));
    const confirmationDialog = useDialog(ConfirmDialogModal)

    const canUserUpdateBid = useMemo(() => {
        if (SystemUser.hasAccess(AccessPolicy.BidsUpdate)) return true

        if (bidBlank.createdUserId === SystemUser.id) return true;

        return false;
    }, [bidBlank])

    useEffect(() => {
        async function init() {
            if (props.bidId === null) return setBidBlank(BidBlank.getDefault(SystemUser.id));

            const bid = await BidsProvider.getBidById(props.bidId);

            if (bid === null) return;

            setBidBlank(BidBlank.fromBid(bid));

        }

        if (open) init();

        return () => setBidBlank(BidBlank.getDefault(SystemUser.id));
    }, [props.bidId, open])

    async function saveBidBlank() {
        if (!canUserUpdateBid) return;

        bidBlank.approximateDate = endOfDay(new Date(bidBlank.approximateDate!))

        const result = await BidsProvider.saveBid(bidBlank)

        if (!result.isSuccess) return alert(result.errors[0].message)

        alert('Изменения сохранены');
        handleClose(true);
    }

    async function removeBid(bidId: string | null) {
        if (bidId === null) return;

        const isConfirmed = await confirmationDialog.show({ title: `Вы действительно хотите удалить эту заявку?` })

        if (!isConfirmed) return;

        const result = await BidsProvider.removeBid(bidId);

        if (!result.isSuccess) return alert(result.errors[0].message);

        handleClose(true);
    }

    async function getFile(fileBlank: FileBlank) {
        async function getFileUrl(fileBlank: FileBlank): Promise<string | null> {
            const dataUrl = fileBlank.base64;

            if (String.isNullOrWhitespace(dataUrl)) return null;

            const response = await fetch(dataUrl);
            const blob = await response.blob();

            if (blob === null) return null;

            return URL.createObjectURL(blob);
        }

        const fileUrl = fileBlank.state === FileState.Added
            ? await getFileUrl(fileBlank)
            : fileBlank.url;

        if (String.isNullOrWhitespace(fileUrl)) return;

        const a = document.createElement('a');
        a.href = fileUrl;
        a.download = fileBlank.name;
        document.body.appendChild(a);
        a.click();
        a.remove();
        URL.revokeObjectURL(fileUrl);
    }

    function changeBidFileBlanks(fileBlanks: FileBlank[]) {
        setBidBlank(blank => {
            const bidBlank = { ...blank };
            const bidFileBlanks = [...bidBlank.fileBlanks];

            bidBlank.fileBlanks = bidFileBlanks.concat(fileBlanks);

            return bidBlank;
        })
    }

    function removeBidFileBlanks(removingFileBlanks: FileBlank[], fileIndex?: number) {
        setBidBlank(blank => {
            const bidBlank = { ...blank };
            const bidFileBlanks = [...bidBlank.fileBlanks]

            bidFileBlanks.map((fileBlank, index) => {
                if (
                    (fileIndex === undefined && removingFileBlanks.length === 0) ||
                    (fileIndex === index && removingFileBlanks.map(removingFileBlank => removingFileBlank.path).includes(fileBlank.path))
                )
                    fileBlank.state = FileState.Removed;
            });

            bidBlank.fileBlanks = bidFileBlanks;
            return bidBlank;
        })
    }

    return (
        <Modal isOpen={open} onClose={() => handleClose(false)}>
            <ModalTitle onClose={() => handleClose(false)}>
                {props.bidId !== null ? "Редактирование" : "Создание"} заявки
            </ModalTitle>
            <ModalBody sx={{ width: '40vw', height: '60vh' }}>
                <Box sx={{
                    display: 'flex',
                    gap: 1.5,
                    flexDirection: 'column'
                }}>
                    <InputForm
                        sx={{ marginRight: 2 }}
                        type="text"
                        label='Тема'
                        disabled={!canUserUpdateBid}
                        placeholder='Введите тему'
                        value={bidBlank.title}
                        onChange={(title) => setBidBlank(blank => ({ ...blank, title }))} />
                    <InputForm
                        type="text-area"
                        label='Описание'
                        placeholder='Введите описание'
                        disabled={!canUserUpdateBid}
                        minRows={3}
                        value={bidBlank.description}
                        onChange={(description) => setBidBlank(blank => ({ ...blank, description }))} />
                    <DayPicker locale={ru}
                        ISOWeek mode="single"
                        showOutsideDays
                        disabled={!canUserUpdateBid}
                        footer={'Выберите дату окончания разработки'}
                        selected={new Date(bidBlank.approximateDate!)}
                        onSelect={(approximateDate) => setBidBlank(blank => ({ ...blank, approximateDate }))} />
                    {
                        props.bidId !== null &&
                        <ToggleButtons
                            value={bidBlank.status}
                            exclusive={true}
                            disabled={!canUserUpdateBid}
                            options={Enum.getNumberValues<BidStatus>(BidStatus)}
                            getOptionLabel={(option) => BidStatus.getDisplayName(option)}
                            onChange={(status) => setBidBlank(blank => ({ ...blank, status }))} />
                    }
                    {
                        (props.bidId !== null && bidBlank.status === BidStatus.Denied) &&
                        <InputForm
                            type="text-area"
                            label='Причина отклонения'
                            disabled={!canUserUpdateBid}
                            placeholder='Введите причину отклонения'
                            minRows={3}
                            value={bidBlank.denyDescription}
                            onChange={(denyDescription) => setBidBlank(blank => ({ ...blank, denyDescription }))} />
                    }
                    <Box sx={{ padding: 1, border: '1px solid #dedede', borderRadius: 1 }}>
                        <InputForm
                            title="Загрузить файлы"
                            type="multi-file-input"
                            extensions={[".jpg", ".jpeg", ".png", ".bmp", ".tif", ".webp", ".doc", ".docx"]}
                            disabled={!canUserUpdateBid}
                            fileArea={FileArea.Bid}
                            fileBlanks={bidBlank.fileBlanks}
                            onChange={(fileBlanks) => changeBidFileBlanks(fileBlanks)}
                            getFile={(fileBlank) => getFile(fileBlank)}
                            removeFile={(fileBlanks, fileIndex) => removeBidFileBlanks(fileBlanks, fileIndex)} />
                    </Box>
                </Box>
            </ModalBody>
            {
                canUserUpdateBid &&
                <ModalActions>
                    {
                        props.bidId !== null &&
                        <Tooltip title="Удалить">
                            <IconButton onClick={() => removeBid(props.bidId)}>
                                <DeleteIcon />
                            </IconButton>
                        </Tooltip>
                    }
                    <SaveButton
                        variant="contained"
                        onClick={() => saveBidBlank()} />
                </ModalActions>
            }
        </Modal>
    )
}