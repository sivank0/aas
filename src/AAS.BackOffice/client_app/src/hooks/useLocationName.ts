import { useEffect, useState } from "react";
import { useLocation } from "react-router-dom";
import { UserLinks } from "../app/users/userLinks";
import { LocationName } from "../tools/types/locationName";
import { BidLinks } from "../app/bids/bidLinks";


export function useLocationName() {
    const [locationName, setLocationName] = useState<string | null>(null);
    const location = useLocation();

    useEffect(() => {
        const pathName = location.pathname
        let locationName: string | null = null;

        if (pathName === UserLinks.usersPage)
            locationName = LocationName.getDisplayName(LocationName.Users)

        if (pathName === BidLinks.bidsPage)
            locationName = LocationName.getDisplayName(LocationName.Bids)

        if (pathName === UserLinks.userRolesPage)
            locationName = LocationName.getDisplayName(LocationName.UserRoles)

        if (pathName === UserLinks.userProfile)
            locationName = LocationName.getDisplayName(LocationName.UserProfile)

        setLocationName(locationName);
    }, [location.pathname])

    return locationName;
}