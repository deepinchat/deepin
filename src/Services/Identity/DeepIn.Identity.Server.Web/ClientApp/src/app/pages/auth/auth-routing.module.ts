import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RoutesConfig } from 'src/app/config/routes.config';
import { AuthComponent } from './auth.component';
import { ConfirmEmailComponent } from './confirm-email/confirm-email.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { LockoutComponent } from './lockout/lockout.component';
import { LoginWith2FaComponent } from './login-with-2fa/login-with-2fa.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';

const routes: Routes = [
    {
        path: RoutesConfig.routesNames.auth.root,
        component: AuthComponent,
        children: [
            {
                path: RoutesConfig.routesNames.auth.confirmEmail,
                component: ConfirmEmailComponent
            },
            {
                path: RoutesConfig.routesNames.auth.forgotPassword,
                component: ForgotPasswordComponent
            },
            {
                path: RoutesConfig.routesNames.auth.signIn,
                component: LoginComponent
            },
            {
                path: RoutesConfig.routesNames.auth.signUp,
                component: RegisterComponent
            },
            {
                path: RoutesConfig.routesNames.auth.resetPassword,
                component: ResetPasswordComponent
            },
            {
                path: RoutesConfig.routesNames.auth.lockout,
                component: LockoutComponent
            },
            {
                path: RoutesConfig.routesNames.auth.loginWithTwoFactor,
                component: LoginWith2FaComponent
            },
            {
                path: '',
                redirectTo: RoutesConfig.routes.home.root,
                pathMatch: 'full'
            }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AuthRoutingModule { }