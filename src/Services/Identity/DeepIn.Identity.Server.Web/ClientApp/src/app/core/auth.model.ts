export interface LoginModel {
    userName: string;
    password: string;
    rememberLogin: boolean;
}

export interface RegisterModel {
    email: string;
    password: string;
    confirmPassword: boolean;
}

export interface LoginWithTwoFactorModel {
    twoFactorCode: string;
    rememberMe: boolean;
    rememberMachine: boolean;
}