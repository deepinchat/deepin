import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { RoutesConfig } from '../config/routes.config';
import { LoginModel, LoginWithTwoFactorModel, RegisterModel } from './auth.model';

@Injectable()
export class AuthService {
  constructor(
    private httpClient: HttpClient
  ) {
  }

  isAuthorized() {
    return this.httpClient.get(`${environment.baseUrl}/api/account/checkSession`);
  }

  register(model: RegisterModel) {
    return this.httpClient.post<{ id: string }>(`${environment.baseUrl}/api/account/register`, model);
  }

  login(model: LoginModel) {
    return this.httpClient.post<void>(`${environment.baseUrl}/api/account/login`, model);
  }

  logout() {
    return this.httpClient.post<void>(`${environment.baseUrl}/api/account/logout`, null);
  }

  externalLogin() {
    return this.httpClient.get(`${environment.baseUrl}/api/account/externalCallback`);
  }

  confirmEmail(model: { userId: string, code: string }) {
    return this.httpClient.post(RoutesConfig.endPoints.account.confirmEmail, model);
  }

  resendEmailConfirmation(id: string) {
    return this.httpClient.get(RoutesConfig.endPoints.account.resendEmailConfirmation(id));
  }

  loginWith2fa(model: LoginWithTwoFactorModel) {
    return this.httpClient.post(RoutesConfig.endPoints.account.loginWith2fa, model);
  }

  forgotPassword(model: { email: string }) {
    return this.httpClient.post(RoutesConfig.endPoints.account.forgotPassword, model);
  }

  resetPassword(model: { email: string, code: string, password: string, confirmPassword: string }) {
    return this.httpClient.post(RoutesConfig.endPoints.account.resetPassword, model);
  }
}
