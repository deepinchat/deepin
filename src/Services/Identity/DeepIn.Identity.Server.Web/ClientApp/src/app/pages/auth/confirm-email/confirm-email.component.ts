import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { RoutesConfig } from 'src/app/config/routes.config';
import { AuthService } from 'src/app/core/auth.service';
import { NzMessageService } from 'ng-zorro-antd/message';

@Component({
  selector: 'app-confirm-email',
  templateUrl: './confirm-email.component.html',
  styleUrls: ['./confirm-email.component.scss']
})
export class ConfirmEmailComponent implements OnInit {
  form?: FormGroup;
  loading = false;
  returnUrl: any;
  id: any;
  routers = RoutesConfig.routes;
  sending = false;
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder,
    private authService: AuthService,
    private messageService: NzMessageService) {
    this.route.queryParamMap.subscribe(p => {
      if (p.has('returnUrl')) {
        this.returnUrl = p.get('returnUrl');
      }
      if (p.has('id')) {
        this.id = p.get('id');
      }
    });
  }

  private buildForm() {
    this.form = this.fb.group({
      userId: this.fb.control(this.id),
      code: this.fb.control('', [Validators.required, Validators.minLength(6), Validators.maxLength(6)]),
    });
  }
  ngOnInit() {
    this.buildForm();
  }

  resendEmailConfirmation() {
    if (this.sending) {
      return;
    }
    this.sending = true;
    firstValueFrom(this.authService.resendEmailConfirmation(this.id))
      .then(() => {
        this.messageService.info('The verification code was sent.');
      })
      .finally(() => this.sending = false);
  }
  onSubmit() {
    if (this.loading || this.form?.invalid) {
      return;
    }
    this.loading = true;
    firstValueFrom(this.authService.confirmEmail(this.form?.value))
      .then(() => {
        this.router.navigate([RoutesConfig.routes.auth.signIn], {
          queryParams: {
            returnUrl: this.returnUrl
          }
        });
      })
      .finally(() => {
        this.loading = false;
      });
  }
}
