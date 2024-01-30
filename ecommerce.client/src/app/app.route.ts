import { Routes } from '@angular/router';
import { ADMIN, LOGIN, SALES } from './app.route.names';

export const APP_ROUTES: Routes = [
  {
    path: '',
    loadChildren: () => import('./sales/sales.module').then((m) => m.SalesModule),
  },
  {
    path: ADMIN,
    loadChildren: () => import('./admin-panel/admin-panel.module').then((m) => m.AdminPanelModule),
  },
  {
    path: SALES,
    loadChildren: () => import('./sales/sales.module').then((m) => m.SalesModule),
  },
  {
    path: LOGIN,
    loadChildren: () => import('./login/login.module').then((m) => m.LoginModule),
  }
]