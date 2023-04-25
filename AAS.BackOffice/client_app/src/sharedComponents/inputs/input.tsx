import React from "react";
import {IProps as TextInputProps, TextInput} from "./textInput/textInput";
import {IProps as TextAreaProps, TextArea} from './textArea/textArea';
import {IProps as NumberInputProps, NumberInput} from './numberInput/numberInput';
import {IProps as AutocompleteProps, Autocomplete} from "./selects/select";
import {IProps as MultiAutocompleteProps, MultiAutocomplete} from "./selects/multiSelect";
import {IProps as CheckBoxProps, CheckBox} from "./checkBox/checkBox";

type NumberInputPropsType = { type: 'number' } & NumberInputProps;
type TextInputPropsType = { type: 'text' } & TextInputProps;
type PasswordInputPropsType = { type: 'password' } & TextInputProps;
type TextAreaInputPropsType = { type: 'text-area' } & TextAreaProps;
type AutocompletePropsType<T> = { type: "select" } & AutocompleteProps<T>;
type MultiAutocompletePropsType<T> = { type: "multi-select" } & MultiAutocompleteProps<T>;
type CheckBoxPropsType = { type: "checkBox" } & CheckBoxProps;

export type IProps<T> =
    (
        TextInputPropsType |
        PasswordInputPropsType |
        TextAreaInputPropsType |
        NumberInputPropsType |
        AutocompletePropsType<T> |
        MultiAutocompletePropsType<T> |
        CheckBoxPropsType
        )

export function Input<T>(props: IProps<T>) {
    switch (props.type) {
        case 'text':
            return <TextInput {...props} />;
        case 'password':
            return <TextInput {...props} isPassword/>;
        case 'text-area':
            return <TextArea {...props} />;
        case 'number':
            return <NumberInput {...props} />;
        case 'select':
            return <Autocomplete {...props} />;
        case 'multi-select':
            return <MultiAutocomplete {...props} />;
        case 'checkBox':
            return <CheckBox {...props} />;
    }
} 