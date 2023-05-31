import { SxProps, Theme } from "@mui/material";

export const MultiFileInputStyles: SxProps<Theme> = (theme) => ({
    '.outlinedInput, .disabledOutlinedInput': {
        userSelect: "none",
        height: 130,
        "& .MuiInputBase-input": {
            userSelect: "none",
        }
    },
    '.outlinedInput': {
        cursor: "pointer",
        "& .MuiInputBase-input": {
            cursor: "pointer"
        }
    },
    '.disabledOutlinedInput': {
        cursor: "default",
        "& .MuiInputBase-input": {
            cursor: "default"
        }
    },
    '.filesContainer': {
        display: 'flex',
        gap: 3,
        flexWrap: 'wrap',
        marginTop: 2,
        '.fileCard, .disabledFileCard': {
            display: 'flex',
            justifyContent: 'space-between',
            width: '30%',
            alignItems: 'center',
            border: '1px solid #cbc8c8',
            borderRadius: 1,
            padding: 1,

            '.mergedContainer': {
                display: 'flex',
                gap: 1,
                '.croppedTitleContainer': {
                    display: 'inline-grid',
                    alignItems: 'center',
                    '.croppedTitle': {
                        lineHeight: 1
                    }
                }
            },
            '.darkClosedIconButton': {
                '& .MuiSvgIcon-root': {
                    color: "#000"
                }
            },
            ' .lightClosedIconButton': {
                '& .MuiSvgIcon-root': {
                    color: "#fff"
                }
            }
        },
        '.fileCard': {
            cursor: 'pointer',
            opacity: 1,
            ':hover': {
                backgroundColor: '#f1f1f1'
            }
        },
        '.disabledFileCard': {
            cursor: 'default',
            opacity: 0.7
        }
    }
})