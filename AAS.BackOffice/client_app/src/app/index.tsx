import React from 'react';
import { createRoot } from 'react-dom/client';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { DesktopLayout } from '../sharedComponents/layouts/desktopLayout';
import { Auth } from './auth/auth';
import { AuthLinks } from './auth/authLinks';
import { Registration } from './auth/registration';
import { UserLinks } from './users/userLinks';
import { UsersPage } from './users/usersPage';
import "../tools/string/globalStringConstructorUtils";
import { BidLinks } from './bids/bidLinks';
import { BidsPage } from './bids/bidsPage';
import { UserProfile } from './users/userProfile';
import { HomePage } from '../sharedComponents/homePage';

const container = document.getElementById('app');
const root = createRoot(container!);

root.render(
    <BrowserRouter>
        <DesktopLayout>
            <Routes>
                <Route path={AuthLinks.authentification} element={<Auth />} />
                <Route path={AuthLinks.registration} element={<Registration />} />
                <Route path={UserLinks.usersPage} element={<UsersPage />} />
                <Route path={UserLinks.userProfile} element={<UserProfile />} />
                <Route path={BidLinks.bidsPage} element={<BidsPage />} />
                <Route path={"/"} element={<HomePage />} />
            </Routes>
        </DesktopLayout>
    </BrowserRouter>
)