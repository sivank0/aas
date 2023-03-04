import React from "react";
import { SideBarIconType } from "../../tools/types/sidebar/sideBarIconType";
import PeopleIcon from '@mui/icons-material/People';
import BallotIcon from '@mui/icons-material/Ballot';
import LocalPoliceIcon from '@mui/icons-material/LocalPolice';

interface Props {
    type: SideBarIconType;
}

export const SideBarIcon = (props: Props) => {
    switch (props.type) {
        case SideBarIconType.Users: return <PeopleIcon />;
        case SideBarIconType.UserRoles: return <LocalPoliceIcon />;
        case SideBarIconType.Bids: return <BallotIcon />;
    }
}