import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { RoutesConfig } from 'src/app/config/routes.config';
import { AuthService } from 'src/app/core/auth.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss']
})
export class ForgotPasswordComponent implements OnInit {
  form?: FormGroup;
  loading = false;
  constructor(
    private router: Router,
    private fb: FormBuilder,
    private authService: AuthService
  ) { }

  private buildForm() {
    this.form = this.fb.group({
      email: this.fb.control('', [Validators.email, Validators.required]),
    });
  }
  ngOnInit() {
    this.buildForm();
  }

  onSubmit() {
    if (this.loading || this.form?.invalid) {
      return;
    }
    this.loading = true;
    firstValueFrom(
      this.authService.forgotPassword(this.form?.value))
      .then(() => {
        this.router.navigate([RoutesConfig.routes.auth.resetPassword], {
          queryParams: {
            email: this.form?.value?.email
          }
        });
      }).finally(() => this.loading = false)
  }
}
