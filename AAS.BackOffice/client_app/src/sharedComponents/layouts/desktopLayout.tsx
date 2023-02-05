import React from "react";
import "../../content/desktopMain.css";
import { ThemeProvider } from "@emotion/react";
import { createTheme, CssBaseline } from "@mui/material";
import { ruRU } from '@mui/material/locale';
import { SnackbarProvider, useSnackbar } from "notistack";
import { PropsWithChildren } from "react";

interface LayoutProps { }

const theme = createTheme(
    {}, ruRU
);

export const DesktopLayout = (props: PropsWithChildren<LayoutProps>) => {
    const snackBar = useSnackbar();
    return (
        <ThemeProvider theme={theme}>
            <SnackbarProvider maxSnack={3} >
                <CssBaseline />
                {props.children}
            </SnackbarProvider>
        </ThemeProvider>
    )
}