import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NzMessageService } from 'ng-zorro-antd/message';
import { firstValueFrom } from 'rxjs';
import { RoutesConfig } from 'src/app/config/routes.config';
import { AuthService } from 'src/app/core/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  loading = false;
  constructor(
    private authService: AuthService,
    private messageService: NzMessageService
  ) { }

  ngOnInit() {
  }

  onLogout() {
    if (this.loading) return;
    this.loading = true;
    firstValueFrom(this.authService.logout())
      .then(() => {
        location.reload();
      })
      .catch(() => {
        this.messageService.error('logout failed');
      })
      .finally(() => {
        this.loading = false;
      });
  }
}
