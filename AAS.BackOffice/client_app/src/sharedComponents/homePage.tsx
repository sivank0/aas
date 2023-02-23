import React, { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { AuthLinks } from '../app/auth/authLinks';
import { BidLinks } from '../app/bids/bidLinks';
import { InfrastructureLinks } from '../app/infrastructure/components/infrastructureLinks';
import { UserLinks } from '../app/users/userLinks';
import { getCookie } from '../tools/cookie';
import Sidebar from '../tools/sidebar/sidebar';

export const HomePage = () => {
    const navigate = useNavigate();

    useEffect(() => {
        function redirect() {
            const token = getCookie("Token");

            if (String.isNullOrWhitespace(token))
                return navigate(AuthLinks.authentification);

            if (Sidebar.items.find(item => item.url.includes(BidLinks.bidsPage)))
                return navigate(BidLinks.bidsPage);

            if (Sidebar.items.length === 0)
                return navigate(InfrastructureLinks.forbidden);

            navigate(Sidebar.items[0].url);
        }
        redirect();
    }, [])

    return (<></>)
}