import { Button, ButtonGroup, Paper, Typography } from '@mui/material';
import React from 'react';
import { InputForm } from '../inputs/inputForm';

interface Props {
    page: number,
    countInPage: number,
    onChangePage: (page: number) => void;
    onChangeCountInPage: (countInPage: number) => void;
}

export const PaginationButtons = (props: Props) => {

    function nextPage() {
        props.onChangePage(props.page + 1);
    }

    function previousPage() {
        if (props.page <= 1) return;

        props.onChangePage(props.page - 1);
    }

    return (
        <ButtonGroup sx={{ backgroundColor: "#fff" }}>
            <Button variant='contained' onClick={() => previousPage()}>
                Назад
            </Button>
            <Typography
                component={Paper}
                elevation={3}
                sx={{ display: 'flex', alignItems: "center", paddingX: 2 }}>
                {props.page}
            </Typography>
            <Button variant='contained' onClick={() => nextPage()}>
                Вперед
            </Button>
        </ButtonGroup>
    )
}