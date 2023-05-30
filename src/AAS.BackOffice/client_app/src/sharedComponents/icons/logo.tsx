import React from "react";

interface Props {
    size?: number,
    style?: React.CSSProperties
}

export const Logo = (props: Props) => {
    return (
        <svg
            xmlns="http://www.w3.org/2000/svg"
            width={props.size ?? 100}
            height={props.size ?? 100}
            style={props.style}
            viewBox="0 0 100 100"
        >
            <path fill="#009688" d="M0 0H100V100H0z"></path>
            <text
                x="50"
                y="63"
                fill="#fff"
                fontFamily="Tahoma"
                fontSize="40"
                fontWeight="bold"
                textAnchor="middle"
            >
                СПЗ
            </text>
        </svg>
    );
}