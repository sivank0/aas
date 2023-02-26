import LightModeIcon from '@mui/icons-material/LightMode';
import DarkModeIcon from '@mui/icons-material/DarkMode';
import React from "react";

export enum ThemeMode {
    Light = 1,
    Dark = 2
}

export namespace ThemeMode {
    export function getIcon(mode: ThemeMode, size: 'small' | 'medium' | 'large') {
        switch (mode) {
            case ThemeMode.Light: return <LightModeIcon fontSize={size} sx={{ color: '#FFF' }} />
            case ThemeMode.Dark: return <DarkModeIcon fontSize={size} sx={{ color: '#FFF' }} />
        }
    }

    export function getValue(mode: ThemeMode) {
        switch (mode) {
            case ThemeMode.Light: return 'light'
            case ThemeMode.Dark: return 'dark'
        }
    }
}