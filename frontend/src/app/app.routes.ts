import { Routes } from '@angular/router';
import { Login } from './features/auth/login/login';
import { App } from './app';
import { Home } from './features/home/home';

export const routes: Routes = [
    {
        path: '',
        component: Home
    },
    {
        path:'login',
        component: Login
    },
    {
        path:'register',
        loadComponent: () => import('./features/auth/register/register').then(m => m.Register)
    }
];
