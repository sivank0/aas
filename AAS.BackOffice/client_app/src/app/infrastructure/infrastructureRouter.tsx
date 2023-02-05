import React from 'react';
import { InfrastructureLinks } from './components/infrastructureLinks';
import { Forbidden } from './components/forbidden';
import { Route } from 'react-router-dom';

export const InfrastructureRouter = () => {
    return (
        <Route path={InfrastructureLinks.forbidden} element={<Forbidden />} />
    )
}