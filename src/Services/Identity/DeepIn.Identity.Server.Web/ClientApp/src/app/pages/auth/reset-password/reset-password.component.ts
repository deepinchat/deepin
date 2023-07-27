import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ValidatorFn, AbstractControl, ValidationErrors } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { RoutesConfig } from 'src/app/config/routes.config';
import { AuthService } from 'src/app/core/auth.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss']
})
export class ResetPasswordComponent implements OnInit {
  form?: FormGroup;
  loading = false;
  email?: string;
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder,
    private authService: AuthService
  ) {
    this.route.queryParamMap.subscribe(p => {
      if (p.has('email')) {
        this.email = p.get('email') || '';
      }
    });
  }
  public static matchValues(
    matchTo: string
  ) {
    return (control: AbstractControl): ValidationErrors | null => {
      return !!control.parent &&
        !!control.parent.value &&
        control.value === control.parent.get(matchTo)?.value
        ? null
        : { isMatching: false };
    };
  }
  private buildForm() {
    this.form = this.fb.group({
      email: this.fb.control(this.email, [Validators.email, Validators.required]),
      code: this.fb.control('', [Validators.required, Validators.minLength(4), Validators.maxLength(6)]),
      password: this.fb.control('', [Validators.required, Validators.minLength(8), Validators.maxLength(32)]),
      confirmPassword: this.fb.control('', [Validators.required, ResetPasswordComponent.matchValues('password')])
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
      this.authService.resetPassword(this.form?.value))
      .then(() => {
        this.router.navigateByUrl(RoutesConfig.routes.auth.signIn);
      }).finally(() => this.loading = false)
  }

}
