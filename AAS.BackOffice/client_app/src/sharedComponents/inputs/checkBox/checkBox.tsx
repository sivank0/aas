import {Checkbox, FormControlLabel, SxProps, Theme} from '@mui/material';
import React, {ChangeEvent} from 'react';

export interface IProps {
    label?: string;
    required?: boolean;
    disabled?: boolean;
    checked: boolean;
    size?: "small" | "medium";
    color?: "primary" | "secondary" | "error" | "info" | "success" | "warning" | "default";
    onChange: (value: boolean) => void;
    onClick?: React.MouseEventHandler<HTMLButtonElement>;
    sx?: SxProps<Theme>;
    className?: string;
}

export const CheckBox = (props: IProps) => {
    function onChange(_: ChangeEvent<HTMLInputElement> | null, checked: boolean) {
        return props.onChange(checked);
    }

    const color = props.color ?? "primary"

    return (
        <FormControlLabel
            sx={props.sx}
            className={props.className}
            control={
                <Checkbox
                    checked={props.checked}
                    size={props.size}
                    required={props.required}
                    disabled={props.disabled}
                    onClick={props.onClick}
                    onChange={onChange}
                    color={color}/>
            }
            label={props.label}/>
    )
}