import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { RoutesConfig } from '../config/routes.config';
import { AuthService } from './auth.service';

@Injectable()
export class AuthGuard implements CanActivate {
    constructor(
        private router: Router,
        private authService: AuthService
    ) { }

    async canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        const isAuthorized = await firstValueFrom(this.authService.isAuthorized())
            .then(() => {
                return true;
            })
            .catch(ex => {
                this.router.navigate([RoutesConfig.routes.auth.signIn]);
                return false;
            });
        return isAuthorized;
    }

}