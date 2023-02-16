import { SxProps, TextField, Theme } from '@mui/material';
import React, { ChangeEvent } from 'react';

export interface IProps {
    label?: string;
    required?: boolean;
    placeholder: string;
    rows?: number;
    value: string | null;
    disabled?: boolean;
    minRows?: number;
    sx?: SxProps<Theme>;
    onChange: (value: string | null) => void;
}

export const TextArea = (props: IProps) => {
    function onChange(event: ChangeEvent<HTMLTextAreaElement>) {
        let value: string | null = event.target.value

        if (String.isNullOrWhitespace(value)) value = null;

        props.onChange(value);
    }

    return (
        <TextField
            multiline
            variant='outlined'
            label={props.label}
            required={props.required}
            className={'form-control'}
            onChange={onChange}
            placeholder={props.placeholder}
            rows={props.rows}
            value={props.value ?? ""}
            minRows={props.minRows}
            sx={props.sx}
            fullWidth
            disabled={props.disabled}
        />
    );
}
