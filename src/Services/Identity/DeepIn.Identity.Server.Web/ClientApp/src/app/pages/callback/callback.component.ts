import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/core/auth.service';

@Component({
  selector: 'app-callback',
  templateUrl: './callback.component.html',
  styleUrls: ['./callback.component.scss']
})
export class CallbackComponent implements OnInit {
  type?: string | null;
  errorMessage?: string;
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService
  ) {
    this.route.paramMap.subscribe(p => {
      if (p.has('type')) {
        this.type = p.get('type');
      }
    });
  }

  ngOnInit() {
    if (this.type) {
      switch (this.type) {
        case 'external-login':
          {
            this.externalLoginCallback(this.route.snapshot.queryParams['returnUrl'], this.route.snapshot.queryParams['remoteError']);
          }
          break;
      }
    }
  }
  private externalLoginCallback(returnUrl: string, remoteError = '') {
    if (remoteError) {
      this.errorMessage = remoteError;
      return;
    }
    this.authService.externalLogin()
      .subscribe({
        next: (x) => {
          console.log('got value ' + x);
          if (returnUrl) {
            location.href = returnUrl;
          } else {
            this.router.navigate(['/']);
          }
        },
        error(err) { console.error('something wrong occurred: ' + err); },
        complete() { console.log('done'); }
      });
  }
}
