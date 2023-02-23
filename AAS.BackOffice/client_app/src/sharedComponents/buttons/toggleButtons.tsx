import { Box, Typography, ToggleButtonGroup, ToggleButton } from '@mui/material';
import React from 'react';

interface Props<TValue> {
    label?: string;
    value: NonNullable<TValue>;
    size?: "small" | "medium" | "large";
    options: NonNullable<TValue>[];
    getOptionLabel: (option: TValue) => string;
    disabled?: boolean;
    onChange: (value: TValue) => void;
}

export function ToggleButtons<TValue>(props: Props<TValue>) {
    function onChange(value: TValue) {
        if (!value) return
        props.onChange(value)
    }

    return (
        <Box sx={{ width: "100%" }}>
            {
                props.label &&
                <Typography sx={theme => ({ fontSize: 14, marginBottom: 0.3, color: theme.palette.grey[700] })}>{props.label}</Typography>
            }
            <ToggleButtonGroup
                size={props.size}
                value={props.value}
                exclusive
                disabled={props.disabled}
                onChange={(_, value: TValue) => onChange(value)}
                sx={{
                    display: "grid",
                    gridTemplateColumns: `repeat(${props.options.length}, 1fr)`
                }}>
                {
                    props.options.map((option, key) =>
                        <ToggleButton
                            key={key}
                            value={option} >
                            {props.getOptionLabel(option)}
                        </ToggleButton>
                    )
                }
            </ToggleButtonGroup>
        </Box>
    )
}