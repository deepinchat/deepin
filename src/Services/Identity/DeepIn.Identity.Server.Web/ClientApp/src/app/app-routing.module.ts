import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RoutesConfig } from './config/routes.config';
import { AuthGuard } from './core/auth.guard';


const routeNames = RoutesConfig.routesNames;

const routes: Routes = [
    { path: routeNames.auth.root, loadChildren: () => import('./pages/auth/auth.module').then(m => m.AuthModule) },
    {
        path: routeNames.home.root,
        loadChildren: () => import('./pages/home/home.module').then(m => m.HomeModule),
        canActivate: [AuthGuard]
    },
    { path: `${routeNames.callback.root}/:type`, loadChildren: () => import('./pages/callback/callback.module').then(m => m.CallbackModule) },
    { path: '**', redirectTo: routeNames.pageNotFound }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
