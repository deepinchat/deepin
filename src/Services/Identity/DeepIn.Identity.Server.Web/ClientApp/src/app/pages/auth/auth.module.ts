import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzSpaceModule } from 'ng-zorro-antd/space';
import { NzTypographyModule } from 'ng-zorro-antd/typography';
import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { SharedModule } from 'src/app/shared/shared.module';
import { AuthComponent } from './auth.component';
import { AuthRoutingModule } from './auth-routing.module';
import { LockoutComponent } from './lockout/lockout.component';
import { LoginWith2FaComponent } from './login-with-2fa/login-with-2fa.component';
import { ConfirmEmailComponent } from './confirm-email/confirm-email.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { NzMessageServiceModule } from 'ng-zorro-antd/message';


@NgModule({
  imports: [
    RouterModule,
    NzFormModule,
    NzButtonModule,
    NzInputModule,
    NzLayoutModule,
    NzCheckboxModule,
    NzGridModule,
    NzSpaceModule,
    NzTypographyModule,
    SharedModule,
    AuthRoutingModule,
    NzMessageServiceModule
  ],
  declarations: [
    AuthComponent,
    LockoutComponent,
    LoginWith2FaComponent,
    ConfirmEmailComponent,
    ForgotPasswordComponent,
    LoginComponent,
    RegisterComponent,
    ResetPasswordComponent
  ]
})
export class AuthModule { }
