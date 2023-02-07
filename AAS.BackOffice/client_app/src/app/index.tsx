import React from 'react';
import { createRoot } from 'react-dom/client';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { DesktopLayout } from '../sharedComponents/layouts/desktopLayout';
import { Auth } from './auth/auth';
import { Registration } from './registration/registration';

const container = document.getElementById('app');
const root = createRoot(container!);

root.render(
    <BrowserRouter>
        <DesktopLayout>
            <Routes>
                <Route path="/" element={<Registration />} />
            </Routes>
        </DesktopLayout>
    </BrowserRouter>
)