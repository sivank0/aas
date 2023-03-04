import { Autocomplete as MaterialAutocomplete, AutocompleteOwnerState, AutocompleteRenderGetTagProps, AutocompleteRenderOptionState, SxProps, TextField } from '@mui/material';
import React, { useMemo } from 'react';
import { newId } from '../../../tools/types/id';

export interface IProps<TValue> {
    label?: string;
    required?: boolean;
    value: TValue | null;
    options: TValue[];
    size?: "small" | "medium" | undefined;
    sx?: SxProps;
    getOptionLabel: (option: TValue) => string;
    disableClearable?: boolean;
    disabled?: boolean;
    placeholder?: string;
    isOptionEqualToValue?: (first: TValue, second: TValue) => boolean;
    onChange: (value: TValue | null) => void;
    renderOption?: (props: React.HTMLAttributes<HTMLLIElement>, option: TValue, state: AutocompleteRenderOptionState) => React.ReactNode;
    renderTags?: ((value: TValue[], getTagProps: AutocompleteRenderGetTagProps, ownerState: AutocompleteOwnerState<TValue, undefined, boolean, undefined, "div">) => React.ReactNode) | undefined;
}

export function Autocomplete<TValue>(props: IProps<TValue>) {

    function isOptionEqualToValue(first: TValue, second: TValue) {
        if (props.isOptionEqualToValue)
            return props.isOptionEqualToValue(first, second);
        return first === second;
    }

    function onChange(_: React.SyntheticEvent<Element, Event>, value: TValue | null) {
        props.onChange(value)
    }

    const id = useMemo(() => newId(), [])

    return (
        <MaterialAutocomplete
            size={props.size ?? "medium"}
            placeholder={props.placeholder}
            onChange={onChange}
            options={props.options}
            value={props.value}
            sx={props.sx}
            isOptionEqualToValue={isOptionEqualToValue}
            getOptionLabel={props.getOptionLabel}
            disableClearable={props.disableClearable}
            disabled={props.disabled}
            noOptionsText="Пусто"
            renderOption={props.renderOption}
            renderTags={props.renderTags}
            renderInput={(params) => (
                <TextField
                    {...params}
                    label={props.label}
                    required={props.required}
                    inputProps={{
                        ...params.inputProps,
                        autoComplete: 'new-password',
                        id: id,
                    }} />
            )} />
    )
}