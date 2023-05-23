import FileUploadOutlinedIcon from '@mui/icons-material/FileUploadOutlined'
import { Box, Button, FormControl, Grid, InputAdornment, InputLabel, OutlinedInput, Typography } from '@mui/material'
import { SxProps, Theme } from '@mui/material/styles'
import React, { DragEvent, useEffect, useMemo, useRef, useState } from 'react'
import { FileState } from '../../../domain/files/enums/fileState'
import { FileBlank } from '../../../domain/files/fileBlank'
import useDialog from '../../../hooks/useDialog'
import { BrowserType } from '../../../tools/browserType'
import { ConfirmDialogModal } from '../../modals/modal'
import { readAsDataUrl } from '../../../tools/utils/fileReader'

export interface IProps extends FileProps {
    title: string,
    disabled?: boolean,
    sx?: SxProps<Theme>,
    onChange: (files: FileBlank[]) => void;
    getFile: (fileBlank: FileBlank) => void;
    removeFile: (fileBlanks: FileBlank[], fileIndex?: number) => void;
}

interface FileProps {
    fileBlanks: FileBlank[],
    extensions?: string[],
    imageWidth?: number,
    imageHeight?: number,
}

const imageExtensions = [".jpg", ".jpeg", ".png", ".bmp", ".tif", ".webp"];

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
        const { imageWidth, imageHeight, extensions } = props
        const index = file.name.lastIndexOf('.');

        const extension = index > 0 ? file.name.slice(index).toLowerCase() : file.type?.split('/')[1]?.toLowerCase()
        const fileName = file.name.replace(extension, "");

        if (extensions !== undefined && extensions.length !== 0 && !extensions.some(ext => ext?.toLowerCase() === extension)) {
            alert(
                `Неверное расширение файла ${extension}. Можно загружать файлы с ${extensions.length === 1
                    ? `расширением ${extensions[0]}`
                    : `расширениями: ${extensions.map(ext => ext).join(', ')}`}`
            );
            return null;
        }

        const base64 = await readAsDataUrl(file)

        if ((imageWidth != null && imageHeight != null) && imageExtensions.includes(extension)) {
            const checkSize = new Promise<boolean>(resolve => {
                const image = new Image()

                image.onload = () => resolve(image.width === imageWidth && image.height === imageHeight)
                image.onerror = () => resolve(false)
                image.src = base64
            })

            if (!await checkSize) {
                alert(`Размер загружаемого файла должен быть ${imageWidth} пикселей на ${imageHeight} пикселей`);
                return null;
            }
        }

        return FileBlank.create(fileName, file.type, base64);
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
    console.log(props.fileBlanks)
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
                                item xl={3} lg={4} xs={6}
                                key={`upload-file--${index} `}
                                sx={{
                                    mb: 0,
                                    display: 'flex',
                                    flexDirection: 'column',
                                    alignItems: 'center'
                                }}>
                                <Box>
                                    <Typography>{fileBlank.name}{fileBlank.extension}</Typography>
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