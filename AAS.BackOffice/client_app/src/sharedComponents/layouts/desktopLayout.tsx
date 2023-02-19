import React from "react";
import "../../content/desktopMain.css";
import { ThemeProvider } from "@emotion/react";
import { AppBar, Box, createTheme, CssBaseline } from "@mui/material";
import { ruRU } from '@mui/material/locale';
import { SnackbarProvider } from "notistack";
import { PropsWithChildren } from "react";
import { DesktopAppBar } from "../desktopAppBar";
import SystemUser from "../../domain/systemUser";

interface LayoutProps { }

const theme = createTheme(
    {}, ruRU
);

export const DesktopLayout = (props: PropsWithChildren<LayoutProps>) => {
    return (
        <ThemeProvider theme={theme}>
            <SnackbarProvider maxSnack={3} >
                <CssBaseline />
                {SystemUser !== null &&
                    <DesktopAppBar />
                }
                <Box sx={{ marginTop: 2 }}>
                    {props.children}
                </Box>
            </SnackbarProvider>
        </ThemeProvider>
    )
}