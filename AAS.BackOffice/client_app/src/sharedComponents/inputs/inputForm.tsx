import React from 'react';
import { Input, IProps as InputProps } from './input';

type IProps<T> = {
    label?: string | null | undefined
} & InputProps<T>;

export function InputForm<T>(props: IProps<T>) {
    return (
        <Input {...props} />
    )
}
