import React from "react";
import "../../content/desktopMain.css";
import { ThemeProvider } from "@emotion/react";
import { Box, createTheme, CssBaseline } from "@mui/material";
import { ruRU } from '@mui/material/locale';
import { SnackbarProvider } from "notistack";
import { PropsWithChildren } from "react";
import { DesktopAppBar } from "../desktopAppBar";
import DialogProvider from "../modals/async/DialogProvider";
import SystemUser from "../../domain/systemUser";

interface LayoutProps { }

const theme = createTheme(
    {}, ruRU
);

export const DesktopLayout = (props: PropsWithChildren<LayoutProps>) => {
    return (
        <ThemeProvider theme={theme}>
            <SnackbarProvider maxSnack={3} >
                <DialogProvider>
                    <CssBaseline />
                    {
                        SystemUser !== null &&
                        <DesktopAppBar />
                    }
                    <Box sx={{
                        width: SystemUser === null ? "100%" : "calc(100% - 65px)",
                        height: SystemUser === null ? "100%" : "calc(100% - 65px)",
                        marginTop: SystemUser === null ? 0 : "89px",
                        marginLeft: SystemUser === null ? 0 : "65px"
                    }}>
                        {props.children}
                    </Box>
                </DialogProvider>
            </SnackbarProvider>
        </ThemeProvider>
    )
}