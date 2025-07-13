import { Routes } from '@angular/router';
import { LayoutComponent } from './layout/layout';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./pages/home/home').then(m => m.HomeComponent),
  },
  {
    path: 'about',
    loadComponent: () => import('./pages/about/about').then(m => m.AboutComponent),
  },
];
