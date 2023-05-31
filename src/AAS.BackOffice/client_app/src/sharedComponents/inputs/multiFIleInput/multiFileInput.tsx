import FileUploadOutlinedIcon from '@mui/icons-material/FileUploadOutlined'
import InsertDriveFileIcon from '@mui/icons-material/InsertDriveFile'
import { Box, Button, FormControl, InputAdornment, InputLabel, OutlinedInput, Tooltip, Typography } from '@mui/material'
import { SxProps, Theme } from '@mui/material/styles'
import React, { DragEvent, useEffect, useMemo, useRef, useState } from 'react'
import { FileArea } from '../../../domain/files/enums/fileArea'
import { FileState } from '../../../domain/files/enums/fileState'
import { FileBlank } from '../../../domain/files/fileBlank'
import useDialog from '../../../hooks/useDialog'
import { readAsDataUrl } from '../../../tools/utils/fileReader'
import { CloseIconButton } from '../../buttons/button'
import { ConfirmDialogModal } from '../../modals/modal'
import { MultiFileInputStyles } from './multiFileInputStyles'

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
        if (files === null || props.disabled) return;

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
        if (props.disabled) return;

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
        <FormControl sx={MultiFileInputStyles} fullWidth disabled={props.disabled}>
            <InputLabel shrink htmlFor={props.title}>{props.title}</InputLabel>
            <OutlinedInput
                {...dropProps}
                id={props.title}
                label={props.title}
                disabled={props.disabled}
                title={props.title}
                className={
                    props.disabled
                        ? 'disabledOutlinedInput'
                        : 'outlinedInput'
                }
                onClick={() => inputRef.current?.click()}
                startAdornment={<InputAdornment position="start"><FileUploadOutlinedIcon /></InputAdornment>}
                value='Перетащите сюда файл или нажмите здесь чтобы загрузить его'
                multiline
                readOnly />
            <input
                type="file"
                disabled={props.disabled}
                accept={props.extensions?.join(',')}
                hidden
                ref={inputRef}
                multiple
                onChange={e => renderPreview(e.target.files)} />
            {
                (isInit && fileBlanks.length !== 0) &&
                <Box className='filesContainer'>
                    {
                        fileBlanks.filter(fileBlank => fileBlank.state !== FileState.Removed).map((fileBlank, index) =>
                            <Box
                                key={`upload-file--${index} `}
                                className={
                                    props.disabled
                                        ? 'disabledFileCard'
                                        : 'fileCard'
                                }
                                onClick={() =>
                                    props.disabled
                                        ? undefined
                                        : props.getFile(fileBlank)
                                } >
                                <Box className='mergedContainer'>
                                    <InsertDriveFileIcon color='primary' />
                                    <Box className='croppedTitleContainer'>
                                        <Tooltip title={fileBlank.name}>
                                            <Typography className='croppedTitle' noWrap>
                                                {fileBlank.name}
                                            </Typography>
                                        </Tooltip>
                                    </Box>
                                </Box>
                                {
                                    !props.disabled &&
                                    <CloseIconButton
                                        title='Удалить файл'
                                        className='darkClosedIconButton'
                                        onClick={() => removeFile(index)} />
                                }
                            </Box>
                        )}
                </Box>
            }
            {
                fileBlanks.filter(fileBlank => fileBlank.state !== FileState.Removed).length !== 0 &&
                <Typography component="div" align="right" sx={{ mt: 1 }}>
                    <Button
                        size="small"
                        disabled={props.disabled}
                        onClick={() => removeFile()}>
                        Удалить всё
                    </Button>
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