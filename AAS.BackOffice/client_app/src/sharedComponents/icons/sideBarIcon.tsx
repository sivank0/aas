import React from "react";
import { SideBarIconType } from "../../tools/sidebar/sideBarIconType";
import PeopleIcon from '@mui/icons-material/People';
import BallotIcon from '@mui/icons-material/Ballot';

interface Props {
    type: SideBarIconType;
}

export const SideBarIcon = (props: Props) => {
    switch (props.type) {
        case SideBarIconType.Users: return <PeopleIcon />;
        case SideBarIconType.Bids: return <BallotIcon />;
    }
}