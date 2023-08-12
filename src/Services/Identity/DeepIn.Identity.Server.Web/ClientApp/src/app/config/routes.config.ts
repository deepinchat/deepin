import { environment } from "src/environments/environment";

const routesNames = {
    pageNotFound: '404',
    auth: {
        root: '',
        confirmEmail: 'confirm-email',
        forgotPassword: 'forgot-password',
        signIn: 'signin',
        signUp: 'signup',
        resetPassword: 'reset-password',
        lockout: 'lockout',
        loginWithTwoFactor: 'login-with-2factor'

    },
    home: {
        root: 'home',
        index: 'index'
    },
    callback: {
        root: 'callback',
        externalLogin: 'external-login'
    }
};
const endPoints = {
    account: {
        confirmEmail: `${environment.baseUrl}/api/account/confirmEmail`,
        resendEmailConfirmation: (id: string) => `${environment.baseUrl}/api/account/resendEmailConfirmation?id=${id}`,
        loginWith2fa: `${environment.baseUrl}/api/account/loginWith2fa`,
        forgotPassword: `${environment.baseUrl}/api/account/forgotPassword`,
        resetPassword: `${environment.baseUrl}/api/account/resetPassword`,
    },
  challenge: {
    externalLogin: (provider: string, returnUrl = '') => `/Challenge/ExternalLogin?provider=${provider}&returnUrl=${encodeURIComponent(returnUrl)}`
    }
}

export const RoutesConfig = {
    endPoints,
    routesNames,
    routes: {
        pageNotFound: `/${routesNames.pageNotFound}`,
        auth: {
            signUp: `/${routesNames.auth.signUp}`,
            signIn: `/${routesNames.auth.signIn}`,
            forgotPassword: `/${routesNames.auth.forgotPassword}`,
            confirmEmail: `/${routesNames.auth.confirmEmail}`,
            resetPassword: `/${routesNames.auth.resetPassword}`,

        },
        home: {
            root: `/${routesNames.home.root}`,
            index: `/${routesNames.home.root}/${routesNames.home.index}`
        }
    }
};
