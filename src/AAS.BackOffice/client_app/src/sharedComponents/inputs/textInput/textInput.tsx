import {InputProps, SxProps, TextField} from "@mui/material";
import {Theme} from "@mui/system";
import React, {ChangeEvent, useMemo} from "react";
import {newId} from "../../../tools/types/id";

export interface IProps {
    label: string;
    required?: boolean;
    autoComplete?: boolean;
    isError?: boolean;
    debounce?: [handler: (value: string) => void, timeout: number];
    disabled?: boolean;
    isPassword?: boolean;
    placeholder?: string;
    hiddenLabel?: boolean;
    value: string | null;
    size?: "small" | "medium";
    variant?: "standard" | "filled" | "outlined" | undefined;
    sx?: SxProps<Theme>;
    className?: string;
    fullWidth?: boolean;
    onChange: (value: string | null) => void;
    InputProps?: InputProps;
}

export const TextInput = (props: IProps) => {
    function onChange(event: ChangeEvent<HTMLInputElement>) {
        let value: string | null = event.target.value;

        if (String.isNullOrWhitespace(value)) value = null;

        props.onChange(value);
    }

    const id = useMemo(() => newId(), [])

    return (
        <TextField
            id={id}
            error={props.isError}
            label={props.label}
            required={props.required}
            autoComplete={'new-password' ?? props.autoComplete}
            className={props.className}
            size={props.size}
            fullWidth={props.fullWidth ?? true}
            sx={props.sx}
            hiddenLabel={props.hiddenLabel}
            disabled={props.disabled}
            onChange={onChange}
            type={`${props.isPassword ? 'password' : 'text'}`}
            placeholder={props.placeholder}
            variant={props.variant ?? "outlined"}
            value={props.value ?? ""}
            InputProps={props.InputProps}/>
    )
}