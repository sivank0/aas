import { Avatar, Box, Fade, SvgIconTypeMap, Typography, } from '@mui/material';
import { OverridableComponent } from '@mui/material/OverridableComponent';
import React, { ChangeEvent, useEffect, useMemo, useState } from 'react';
import { FileBlank } from '../../../domain/files/fileBlank';
import { FileState } from '../../../domain/files/enums/fileState'; 
import { CloseIconButton } from '../../buttons/button';

export interface IProps {
    fileBlank: FileBlank | null;
    acceptTypes?: string;
    addImage: (image: FileBlank) => void;
    removeImage: (image: FileBlank | null) => void;
    size: number;
    defaultImage: DefaultImageProps;
    alignItem?: AlignItem;
}

type AlignItem = {
    marginLeft: 'auto' | undefined,
    marginRight: 'auto' | undefined
}

type DefaultImageProps = {
    image: OverridableComponent<SvgIconTypeMap<{}, "svg">> & {
        muiName: string;
    };
    size: string | number;
    title?: string;
    fontSize?: string | number;
}

export const ImageInput = (props: IProps) => {
    const [isInit, setIsInit] = useState<boolean>(false);

    const imageBlankUrl: string | null = useMemo(() => {
        if (props.fileBlank === null || props.fileBlank.state === FileState.Removed)
            return null;

        return props.fileBlank.url ?? props.fileBlank.base64 ?? null;
    }, [props.fileBlank, props.fileBlank?.state])

    useEffect(() => setIsInit(true), [imageBlankUrl])

    async function onChange(event: ChangeEvent<HTMLInputElement>) {
        const readFile = (file: File): Promise<string | ArrayBuffer | null> => new Promise((resolve, reject) => {
            const reader = new FileReader();
            reader.readAsDataURL(file);
            reader.onload = () => resolve(reader.result);
            reader.onerror = error => reject(error);
        });

        let file = event.target.files![0];

        if (file == null) return;

        const fileNameParts = file.name.split('.');
        const fileExtension = `.${fileNameParts[fileNameParts.length - 1]}`;
        const fileName = file.name.replace(fileExtension, "");
        let fileBase64 = await readFile(file) as string;

        props.addImage(FileBlank.create(fileName, fileBase64))
    }

    function removeImage() {
        if (props.fileBlank === null) return;
        
        let fileBlank: FileBlank | null = props.fileBlank;

        switch(props.fileBlank.state){
            case FileState.Added:{
                fileBlank = null;
                break;
            }
            case FileState.Intact:{
                fileBlank.state = FileState.Removed;
                break;
            }
            case FileState.Removed: return;
        }
        props.removeImage(fileBlank);
    }

    return (
        <Fade in={isInit} timeout={1000}>
            <Box
                sx={{
                    position: "relative",
                    width: "fit-content",
                    display: "flex",
                    marginLeft: props.alignItem?.marginLeft,
                    marginRight: props.alignItem?.marginRight
                }}>
                <Box sx={{ cursor: "pointer" }} component="label">
                    <Avatar
                        sx={{
                            width: imageBlankUrl === null ? props.size : "auto",
                            height: props.size,
                            border: "1px solid #ddd"
                        }}
                        variant="rounded"
                        src={imageBlankUrl ?? undefined}>
                        <Box
                            sx={{
                                height: "100%",
                                display: "flex",
                                flexDirection: "column",
                                alignItems: "center"
                            }}>
                            <props.defaultImage.image sx={{ fontSize: props.defaultImage.size, marginY: 'auto' }} />
                            <Typography
                                sx={{
                                    textAlign: 'center',
                                    fontSize: props.defaultImage.fontSize ?? 12,
                                    lineHeight: 1,
                                    marginBottom: "12px",
                                    marginTop: "auto"
                                }}>
                                {props.defaultImage.title ?? "Нажмите чтобы выбрать изображение"}
                            </Typography>
                        </Box>
                    </Avatar>
                    <input
                        hidden
                        accept={props.acceptTypes}
                        onChange={onChange}
                        multiple
                        type="file" />
                </Box>
                {
                    imageBlankUrl !== null &&
                    <CloseIconButton
                        title='Удалить изображение'
                        onClick={removeImage}
                        size="small"
                        sx={(theme) => ({
                            position: "absolute",
                            top: 8,
                            right: 8,
                            color: theme.palette.error.main,
                            backgroundColor: theme.palette.error.contrastText,
                            opacity: 0.7,
                            ":hover": {
                                backgroundColor: theme.palette.error.contrastText,
                                opacity: 0.9,
                            }
                        })}  />
                }
            </Box>
        </Fade>
    )
}
