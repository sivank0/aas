import {Box, CssBaseline, ThemeProvider, createTheme} from "@mui/material";
import {ruRU} from '@mui/material/locale';
import React, {PropsWithChildren, useEffect, useState} from "react";
import "../../content/desktopMain.css";
import SystemUser from "../../domain/systemUser";
import {DesktopAppBar} from "../desktopAppBar";
import DialogProvider from "../modals/async/DialogProvider";
import {ThemeMode} from "../themeMode";

interface LayoutProps {
}

const themeModeKey = 'themeMode'

export const DesktopLayout = (props: PropsWithChildren<LayoutProps>) => {
    const [themeMode, setThemeMode] = useState<ThemeMode>(ThemeMode.Light);
    const [isInit, setIsInit] = useState<boolean>(false)

    const theme = createTheme(
        {
            palette: {
                mode: themeMode === ThemeMode.Light
                    ? 'light'
                    : 'dark',
                primary: {
                    main: '#009688',
                },
                secondary: {
                    main: '#9c27b0',
                },
            },
        }, ruRU
    );

    useEffect(() => {
        const localThemeMode = localStorage.getItem(themeModeKey)

        if (String.isNullOrWhitespace(localThemeMode)) {
            setIsInit(true)
            return localStorage.setItem(themeModeKey, ThemeMode.getValue(ThemeMode.Light))
        }

        const themeMode = localThemeMode === ThemeMode.getValue(ThemeMode.Light)
            ? ThemeMode.Light
            : ThemeMode.Dark
        setThemeMode(themeMode)
        setIsInit(true)
    }, [])

    function changeThemeMode(mode: ThemeMode) {
        setThemeMode(mode)
        localStorage.setItem(themeModeKey, ThemeMode.getValue(mode))

    }

    return (
        <ThemeProvider theme={theme}>
            <DialogProvider>
                {
                    isInit &&
                    <>
                        <CssBaseline/>
                        {
                            SystemUser !== null &&
                            <DesktopAppBar themeMode={themeMode} changeThemeMode={(mode) => changeThemeMode(mode)}/>
                        }
                        <Box sx={{
                            width: SystemUser === null ? "100%" : "calc(100% - 65px)",
                            height: SystemUser === null ? "100%" : "calc(100% - 65px)",
                            marginTop: SystemUser === null ? 0 : "65px",
                            marginLeft: SystemUser === null ? 0 : "65px"
                        }}>
                            {props.children}
                        </Box>
                    </>
                }
            </DialogProvider>
        </ThemeProvider>
    )
}