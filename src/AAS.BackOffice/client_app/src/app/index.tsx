import React from 'react';
import { createRoot } from 'react-dom/client';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { HomePage } from '../sharedComponents/homePage';
import { DesktopLayout } from '../sharedComponents/layouts/desktopLayout';
import "../tools/utils/globalStringConstructorUtils";
import { Auth } from './auth/auth';
import { AuthLinks } from './auth/authLinks';
import { Registration } from './auth/registration';
import { BidLinks } from './bids/bidLinks';
import { BidsPage } from './bids/bidsPage';
import { EmailVerificationPage } from './infrastructure/components/emailVerificationPage';
import { InfrastructureLinks } from './infrastructure/components/infrastructureLinks';
import { UserRolesPage } from './users/roles/userRolesPage';
import { UserLinks } from './users/userLinks';
import { UserProfile } from './users/userProfile';
import { UsersPage } from './users/usersPage';

const container = document.getElementById('app');
const root = createRoot(container!);

root.render(
    <BrowserRouter>
        <DesktopLayout>
            <Routes>
                <Route path={AuthLinks.authentification} element={<Auth />} />
                <Route path={AuthLinks.registration} element={<Registration />} />
                <Route path={InfrastructureLinks.emailVerification} element={<EmailVerificationPage />} />
                <Route path={UserLinks.usersPage} element={<UsersPage />} />
                <Route path={UserLinks.userRolesPage} element={<UserRolesPage />} />
                <Route path={UserLinks.userProfile} element={<UserProfile />} />
                <Route path={BidLinks.bidsPage} element={<BidsPage />} />
                <Route path={"/"} element={<HomePage />} />
            </Routes>
        </DesktopLayout>
    </BrowserRouter>
)