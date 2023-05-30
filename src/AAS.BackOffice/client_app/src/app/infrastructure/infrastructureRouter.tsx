import React from 'react';
import { Route } from 'react-router-dom';
import { EmailVerificationPage } from './components/emailVerificationPage';
import { Forbidden } from './components/forbidden';
import { InfrastructureLinks } from './components/infrastructureLinks';
import { NotEnoughPermissions } from './components/notEnoughPermissions';

export const InfrastructureRouter = () => {
    return (
        <>
            <Route path={InfrastructureLinks.forbidden} element={<Forbidden />} />
            <Route path={InfrastructureLinks.emailVerification} element={<EmailVerificationPage />} />
            <Route path={InfrastructureLinks.notEnoughPermissions} element={<NotEnoughPermissions />} />
        </>
    )
}