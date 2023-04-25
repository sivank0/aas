import {SxProps, TextField} from '@mui/material';
import React, {ChangeEvent, useEffect, useRef} from 'react';

export interface IProps {
    label?: string;
    required?: boolean;
    autoComplete?: boolean;
    availableFractionValue?: boolean;
    sx?: SxProps;
    disabled?: boolean;
    min?: number;
    max?: number;
    placeholder: string;
    step?: number;
    value: number | null;
    onChange: (value: number | null) => void;
}

export const NumberInput = (props: IProps) => {
    let prevProps = useRef<IProps>(props);
    useEffect(() => {
        if (props.min != null && props.max != null && props.min >= props.max)
            throw 'Некорректное значение min или max!';

        let value = prevProps.current.value;

        if (prevProps.current.value !== null && value !== null) {
            if (props.min != null && prevProps.current.value < props.min) value = props.min;
            if (props.max != null && prevProps.current.value > props.max) value = props.max;
            if (
                props.step != null &&
                prevProps.current.step != null &&
                getNumberDecimalPlaces(props.step) < getNumberDecimalPlaces(prevProps.current.step)
            )
                value = parseFloat(value.toFixed(getNumberDecimalPlaces(props.step)));
            if (!props.availableFractionValue) value = Math.floor(value);
        }
        if (value !== prevProps.current.value) props.onChange(value);
    }, [props])

    function getNumberDecimalPlaces(value: number) {
        let strArray = value.toString().replace(',', '.').split('.');

        return strArray.length > 1 ? strArray[1].length : 0;
    };

    function onChange(event: ChangeEvent<HTMLInputElement>) {
        let inputValue = event.currentTarget.value.replace(',', '.');

        if (inputValue === '') return props.onChange(null);

        let value = props.availableFractionValue ? parseFloat(inputValue) : parseInt(inputValue);

        if (isNaN(value)) return props.onChange(null);
        if (props.min != null && value < props.min) return;
        if (props.max != null && value > props.max) return;
        if (props.availableFractionValue && getNumberDecimalPlaces(value) > getNumberDecimalPlaces(props.step ?? 0))
            return;

        props.onChange(value);
    };

    function displayByIsFraction() {
        if (props.value === null) return '';
        return props.availableFractionValue ? props.value : props.value.toFixed();
    };

    return (
        <TextField
            type='number'
            required={props.required}
            label={props.label}
            autoComplete={props.autoComplete ?? false ? 'on' : 'off'}
            className={'form-control'}
            sx={props.sx}
            fullWidth
            disabled={props.disabled}
            onChange={onChange}
            value={displayByIsFraction()}
            placeholder={props.placeholder}
            variant="outlined"/>
    );
}