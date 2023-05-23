import React from "react";
import { IProps as TextInputProps, TextInput } from "./textInput/textInput";
import { IProps as TextAreaProps, TextArea } from './textArea/textArea';
import { IProps as NumberInputProps, NumberInput } from './numberInput/numberInput';
import { IProps as AutocompleteProps, Autocomplete } from "./selects/select";
import { IProps as MultiAutocompleteProps, MultiAutocomplete } from "./selects/multiSelect";
import { IProps as CheckBoxProps, CheckBox } from "./checkBox/checkBox";
import { IProps as ImageInputProps, ImageInput } from "./imageInput/imageInput";
import { IProps as MultiFileInputProps, MultiFileInput } from "./multiFIleInput/multiFileInput";

type NumberInputPropsType = { type: 'number' } & NumberInputProps;
type TextInputPropsType = { type: 'text' } & TextInputProps;
type PasswordInputPropsType = { type: 'password' } & TextInputProps;
type TextAreaInputPropsType = { type: 'text-area' } & TextAreaProps;
type AutocompletePropsType<T> = { type: "select" } & AutocompleteProps<T>;
type MultiAutocompletePropsType<T> = { type: "multi-select" } & MultiAutocompleteProps<T>;
type CheckBoxPropsType = { type: "checkBox" } & CheckBoxProps;
type ImageInputPropsType = { type: "image-input" } & ImageInputProps;
type MultiFileInputPropsType = { type: "multi-file-input" } & MultiFileInputProps;

export type IProps<T> =
    (
        TextInputPropsType |
        PasswordInputPropsType |
        TextAreaInputPropsType |
        NumberInputPropsType |
        CheckBoxPropsType |
        ImageInputPropsType |
        MultiFileInputPropsType |
        AutocompletePropsType<T> |
        MultiAutocompletePropsType<T>
    )

export function Input<T>(props: IProps<T>) {
    switch (props.type) {
        case 'text':
            return <TextInput {...props} />;
        case 'password':
            return <TextInput {...props} isPassword />;
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
        case 'image-input':
            return <ImageInput {...props} />;
        case 'multi-file-input':
            return <MultiFileInput {...props} />;
    }
} 