import React from 'react';
import { createRoot } from 'react-dom/client';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { DesktopAppBar } from '../sharedComponents/appBar';
import { DesktopLayout } from '../sharedComponents/layouts/desktopLayout';
import { Auth } from './auth/auth';
import { Registration } from './auth/registration';
import { UserPage } from './users/userPage';

const container = document.getElementById('app');
const root = createRoot(container!);
root.render(
    <BrowserRouter>
        <DesktopLayout>
            <Routes>
                <Route path="/" element={<DesktopAppBar />} />
                <Route path='/registration' element={<Registration />} />
                <Route path="/authorization" element={<Auth />} />
            </Routes>
        </DesktopLayout>
    </BrowserRouter>
)