import { Routes } from '@angular/router';
import {AuthLayoutComponent} from './pages/auth/auth-layout/auth-layout';
import {LoginFormComponent} from './pages/auth/login-form/login-form';
import {RegisterFormComponent} from './pages/auth/register-form/register-form';
import {LayoutComponent} from './layout/layout';
import {HomeComponent} from './pages/home/home';
import {AboutComponent} from './pages/about/about';
import {AuthGuard} from './core/guards/auth-guard';

export const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    canActivate: [AuthGuard],
    children: [
      { path: '', component: HomeComponent },
      { path: 'about', component: AboutComponent },
    ]
  },
  {
    path:'',
    component: AuthLayoutComponent,
    children: [
      { path: 'login', component: LoginFormComponent },
      { path: 'register', component: RegisterFormComponent },
    ]
  },
  { path: '**', redirectTo: '' }
];
