import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { RoutesConfig } from 'src/app/config/routes.config';
import { AuthService } from 'src/app/core/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  form?: FormGroup;
  loading = false;
  returnUrl: any;
  routers = RoutesConfig.routes;
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder,
    private authService: AuthService) {
    this.route.queryParamMap.subscribe(p => {
      if (p.has('ReturnUrl')) {
        this.returnUrl = p.get('ReturnUrl');
      }
    });
  }

  private buildForm() {
    this.form = this.fb.group({
      userName: this.fb.control(''),
      password: this.fb.control(''),
      rememberLogin: this.fb.control(false)
    });
  }
  ngOnInit() {
    this.buildForm();
  }

  externalLogin(provider: string) {
    location.href = RoutesConfig.endPoints.challenge.externalLogin(provider, this.returnUrl ?? '');
  }

  onSubmit() {
    if (this.loading || this.form?.invalid) {
      return;
    }
    this.loading = true;
    firstValueFrom(this.authService.login(this.form?.value))
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
