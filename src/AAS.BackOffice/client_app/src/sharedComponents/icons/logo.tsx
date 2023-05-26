import React from "react";

export const Logo = () => {
    return (
        <svg
            xmlns="http://www.w3.org/2000/svg"
            width="100"
            height="100"
            viewBox="0 0 100 100"
        >
            <path fill="#0c4487" d="M0 0H100V100H0z"></path>
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