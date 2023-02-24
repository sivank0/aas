import React from 'react';
import { InfrastructureLinks } from './components/infrastructureLinks';
import { Forbidden } from './components/forbidden';
import { Route } from 'react-router-dom';
import { NotEnoughPermissions } from './components/notEnoughPermissions';

export const InfrastructureRouter = () => {
    return (
        <>
            <Route path={InfrastructureLinks.forbidden} element={<Forbidden />} />
            <Route path={InfrastructureLinks.notEnoughPermissions} element={<NotEnoughPermissions />} />
        </>
    )
}