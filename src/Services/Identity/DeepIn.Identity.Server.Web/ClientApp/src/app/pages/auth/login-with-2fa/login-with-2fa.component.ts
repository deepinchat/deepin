import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { RoutesConfig } from 'src/app/config/routes.config';
import { AuthService } from 'src/app/core/auth.service';

@Component({
  selector: 'app-login-with-2fa',
  templateUrl: './login-with-2fa.component.html',
  styleUrls: ['./login-with-2fa.component.scss']
})
export class LoginWith2FaComponent implements OnInit {
  form?: FormGroup;
  loading = false;
  routers = RoutesConfig.routes;
  returnUrl: any;
  rememberLogin = false;
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder,
    private authService: AuthService) {
    this.route.queryParamMap.subscribe(p => {
      if (p.has('returnUrl')) {
        this.returnUrl = p.get('returnUrl');
      }
      if (p.has('rememberLogin')) {
        this.rememberLogin = p.get('rememberLogin') === 'true';
      }
    });
  }

  private buildForm() {
    this.form = this.fb.group({
      rememberLogin: this.fb.control(this.rememberLogin),
      rememberMachine: this.fb.control(false),
      twoFactorCode: this.fb.control('', [Validators.required, Validators.minLength(6), Validators.maxLength(6)]),
    });
  }

  ngOnInit() {
    this.buildForm();
  }

  resendTwoFactorCode() {

  }

  onSubmit() {
    if (this.loading || this.form?.invalid) {
      return;
    }
    this.loading = true;
    firstValueFrom(this.authService.loginWith2fa(this.form?.value))
      .then(() => {
        if (this.returnUrl) {
          location.href = this.returnUrl;
        } else {
          this.router.navigate([this.routers.home.root]);
        }
      })
      .finally(() => {
        this.loading = false;
      });
  }

}
