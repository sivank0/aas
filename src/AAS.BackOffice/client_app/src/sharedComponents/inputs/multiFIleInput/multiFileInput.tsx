import FileUploadOutlinedIcon from '@mui/icons-material/FileUploadOutlined'
import InsertDriveFileIcon from '@mui/icons-material/InsertDriveFile'
import { Box, Button, FormControl, Grid, InputAdornment, InputLabel, OutlinedInput, Typography } from '@mui/material'
import { SxProps, Theme } from '@mui/material/styles'
import React, { DragEvent, useEffect, useMemo, useRef, useState } from 'react'
import { FileArea } from '../../../domain/files/enums/fileArea'
import { FileState } from '../../../domain/files/enums/fileState'
import { FileBlank } from '../../../domain/files/fileBlank'
import useDialog from '../../../hooks/useDialog'
import { readAsDataUrl } from '../../../tools/utils/fileReader'
import { CloseIconButton } from '../../buttons/button'
import { ConfirmDialogModal } from '../../modals/modal'

export interface IProps extends FileProps {
    title: string,
    disabled?: boolean,
    sx?: SxProps<Theme>,
    onChange: (files: FileBlank[]) => void;
    getFile: (fileBlank: FileBlank) => void;
    removeFile: (fileBlanks: FileBlank[], fileIndex?: number) => void;
}

interface FileProps {
    fileArea: FileArea;
    fileBlanks: FileBlank[],
    extensions?: string[],
}

export function MultiFileInput(props: IProps) {
    const [isInit, setIsInit] = useState<boolean>(false);
    const confirmationDialog = useDialog(ConfirmDialogModal);

    const inputRef = useRef<HTMLInputElement | null>(null)
    const [fileBlanks, setFileBlanks] = useState<FileBlank[]>([])
    const dropProps = useDrop((event) => renderPreview(event?.dataTransfer.files ?? null))

    useEffect(() => {
        function loadFileBlanks() {
            const fileBlanks = props.fileBlanks.filter(fb => fb.state !== FileState.Removed)
            setFileBlanks(fileBlanks);
            setIsInit(true);
        }

        loadFileBlanks();
    }, [props.fileBlanks])

    async function parseFile(file: File, props: FileProps): Promise<FileBlank | null> {
        const index = file.name.lastIndexOf('.');

        const extension = index > 0 ? file.name.slice(index).toLowerCase() : file.type?.split('/')[1]?.toLowerCase()
        const fileName = file.name.replace(extension, "");

        if (props.extensions !== undefined && props.extensions.length !== 0 && !props.extensions.some(ext => ext?.toLowerCase() === extension)) {
            alert(
                `Неверное расширение файла ${extension}. Можно загружать файлы с ${props.extensions.length === 1
                    ? `расширением ${props.extensions[0]}`
                    : `расширениями: ${props.extensions.map(ext => ext).join(', ')}`}`
            );
            return null;
        }

        const base64 = await readAsDataUrl(file)

        return FileBlank.create(fileName, base64, props.fileArea);
    }

    async function renderPreview(files: FileList | null) {
        if (files === null) return;

        const newFileBlanks = new Array<FileBlank>()

        for (let i = 0; i < files.length; i++) {
            const fileBlank = await parseFile(files[i], props)

            if (fileBlank === null) return;

            newFileBlanks.push(fileBlank)
        }

        setFileBlanks(files => files.concat(newFileBlanks))
        props.onChange(newFileBlanks)

        if (inputRef.current != null) inputRef.current.value = ""
    }

    async function removeFile(index?: number) {
        if (index === undefined) {
            const isConfirmed = await confirmationDialog.show({ title: "Вы действительно хотите удалить  все файлы?" })

            if (!isConfirmed) return;
        }

        if (inputRef.current) inputRef.current.value = ''

        setFileBlanks(fileBlanks => {
            const files = [...fileBlanks]
            let removingFileBlanks: FileBlank[] = [];

            if (index === undefined) {
                removingFileBlanks = files;
            }
            else if (index >= 0 || index < fileBlanks.length) {
                files.filter(file => file.state !== FileState.Removed).map((file, fileIndex) => {
                    if (fileIndex === index) removingFileBlanks.push(file);
                })
            }

            props.removeFile(removingFileBlanks, index);
            return files;
        })
    }

    return (
        <FormControl fullWidth>
            <InputLabel shrink htmlFor={props.title}>{props.title}</InputLabel>
            <OutlinedInput
                {...dropProps}
                id={props.title}
                label={props.title}
                title={props.title}
                sx={{ userSelect: "none", cursor: "pointer", height: 130, "& .MuiInputBase-input": { userSelect: "none", cursor: "pointer" } }}
                onClick={() => inputRef.current?.click()}
                startAdornment={<InputAdornment position="start"><FileUploadOutlinedIcon /></InputAdornment>}
                value='Перетащите сюда файл или нажмите здесь чтобы загрузить его'
                multiline
                readOnly />
            <input type="file" accept={props.extensions?.join(',')} hidden ref={inputRef} multiple onChange={e => renderPreview(e.target.files)} />
            {
                (isInit && fileBlanks.length !== 0) &&
                <Grid container spacing={2} sx={{ marginTop: 0 }}>
                    {
                        fileBlanks.filter(fileBlank => fileBlank.state !== FileState.Removed).map((fileBlank, index) =>
                            <Grid
                                key={`upload-file--${index} `}
                                item xl={3} lg={4} xs={6}
                                sx={{
                                    mb: 0,
                                    display: 'flex',
                                    flexDirection: 'column',
                                    alignItems: 'center'
                                }}>
                                <Box
                                    onClick={() => props.getFile(fileBlank)}
                                    sx={{
                                        display: 'flex',
                                        gap: 1,
                                        alignItems: 'center',
                                        border: '1px solid #cbc8c8',
                                        borderRadius: 1,
                                        padding: 1,
                                        cursor: 'pointer',
                                        ':hover': {
                                            backgroundColor: '#f1f1f1'
                                        }
                                    }}>
                                    <InsertDriveFileIcon color='primary' />
                                    <Typography sx={{ lineHeight: 1 }}>{fileBlank.name}</Typography>
                                    <CloseIconButton title='Удалить файл' sx={{ '& .MuiSvgIcon-root': { color: "#000" } }} onClick={() => removeFile(index)} />
                                </Box>
                            </Grid>
                        )}
                </Grid>
            }
            {
                fileBlanks.filter(fileBlank => fileBlank.state !== FileState.Removed).length !== 0 &&
                <Typography component="div" align="right" sx={{ mt: 1 }}>
                    <Button size="small" disabled={props.disabled} onClick={() => removeFile()}>Удалить всё</Button>
                </Typography>
            }
        </FormControl>
    )
}

export interface DropProps<T> {
    onDragEnter: (e: DragEvent<T>) => void
    onDragOver: (e: DragEvent<T>) => void
    onDragLeave: (e: DragEvent<T>) => void
    onDrop: (e: DragEvent<T>) => void
}

export type Callback<T = any> = (_?: T) => any

function useDrop<T>(callback: Callback<DragEvent<T>>): Partial<DropProps<T>> {
    const dropProps = useMemo(() => {
        function onDragEnter(event: DragEvent<T>) {
            event.preventDefault()
        }

        function onDragOver(event: DragEvent<T>) {
            event.stopPropagation()
            event.preventDefault()
        }

        function onDrop(event: DragEvent<T>) {
            event.stopPropagation()
            event.preventDefault()

            if (event.dataTransfer.files) callback(event)

            event.dataTransfer.clearData()
        }

        function onDragLeave() {
            return;
        }

        return { onDragEnter, onDragOver, onDrop, onDragLeave }
    }, [callback])

    return dropProps;
} 