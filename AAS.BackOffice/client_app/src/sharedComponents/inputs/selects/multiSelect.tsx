import { Autocomplete as MaterialAutocomplete, AutocompleteOwnerState, AutocompleteRenderGetTagProps, AutocompleteRenderOptionState, TextField, FilterOptionsState, Theme, SxProps } from '@mui/material';
import React, { useMemo } from 'react';
import { newId } from '../../../tools/types/id';

export interface IProps<TValue> {
    label?: string;
    required?: boolean;
    size?: "small" | "medium"
    sx?: SxProps<Theme>
    placeholder?: string;
    noOptionsText?: string;
    values: TValue[];
    options: TValue[];
    getOptionLabel: (option: TValue) => string;
    disabled?: boolean;
    filterOptions?: ((options: TValue[], state: FilterOptionsState<TValue>) => TValue[]);
    isOptionEqualToValue?: (first: TValue, second: TValue) => boolean;
    onChange: (values: TValue[]) => void;
    renderOption?: (props: React.HTMLAttributes<HTMLLIElement>, option: TValue, state: AutocompleteRenderOptionState) => React.ReactNode;
    renderTags?: ((value: TValue[], getTagProps: AutocompleteRenderGetTagProps, ownerState: AutocompleteOwnerState<TValue, true, undefined, undefined, "div">) => React.ReactNode)
}


export function MultiAutocomplete<TValue>(props: IProps<TValue>) {
    function isOptionEqualToValue(first: TValue, second: TValue) {
        if (props.isOptionEqualToValue)
            return props.isOptionEqualToValue(first, second);
        return first === second;
    }

    function onChange(_: React.SyntheticEvent<Element, Event>, values: TValue[]) {
        props.onChange(values)
    }

    const id = useMemo(() => newId(), [])

    return (
        <MaterialAutocomplete
            multiple
            placeholder={props.placeholder}
            onChange={onChange}
            size={props.size}
            sx={props.sx}
            filterOptions={props.filterOptions}
            disabled={props.disabled}
            options={props.options.filter(option => !props.values.includes(option))}
            value={props.values}
            noOptionsText={props.noOptionsText}
            renderTags={props.renderTags}
            isOptionEqualToValue={isOptionEqualToValue}
            getOptionLabel={props.getOptionLabel}
            renderOption={props.renderOption}
            renderInput={(params) => (
                <TextField
                    {...params}
                    id={id}
                    label={props.label}
                    required={props.required}
                    inputProps={{
                        ...params.inputProps,
                        autoComplete: 'new-password',
                    }} />
            )} />
    )
}