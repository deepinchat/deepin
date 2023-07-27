import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { RoutesConfig } from 'src/app/config/routes.config';
import { AuthService } from 'src/app/core/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  form?: FormGroup;
  loading = false;
  routers = RoutesConfig.routes;
  returnUrl: any;
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder,
    private authService: AuthService) {
    this.route.queryParamMap.subscribe(p => {
      if (p.has('returnUrl')) {
        this.returnUrl = p.get('returnUrl');
      }
    });
  }

  private buildForm() {
    this.form = this.fb.group({
      email: this.fb.control('', [Validators.email, Validators.maxLength(32), Validators.required]),
      password: this.fb.control('', [Validators.required, Validators.minLength(8), Validators.maxLength(32)]),
      confirmPassword: this.fb.control('')
    });
  }
  ngOnInit() {
    this.buildForm();
  }

  async onSubmit() {
    if (this.loading || this.form?.invalid) {
      return;
    }
    this.loading = true;
    await firstValueFrom(this.authService.register(this.form?.value))
      .then(res => {
        this.router.navigate([RoutesConfig.routes.auth.confirmEmail], {
          queryParams: {
            id: res.id,
            returnUrl: this.returnUrl
          }
        });
      })
      .finally(() => {
        this.loading = false;
      });
  }
}
