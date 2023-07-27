import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpEvent, HttpHandler, HttpRequest, HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { ModelStateService } from './services/model-state.service';
import { Router } from '@angular/router';

@Injectable()
export class GlobalHttpInterceptor implements HttpInterceptor {
    constructor(
        private router: Router,
        private modelStateService: ModelStateService) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req)
            .pipe(tap((event: HttpEvent<any>) => { }, (response: HttpErrorResponse) => {
                if (response.status === 302) {
                    location.href = response.error;
                } else if (response.status === 400) {
                    const error = response.error;
                    if (response.error.type === 'https://tools.ietf.org/html/rfc7231#section-6.5.1') {
                        this.modelStateService.set(error.errors);
                    } else {
                        if (error instanceof Blob) {
                            const fr = new FileReader();
                            fr.readAsText(error);
                            fr.onloadend = (res) => {
                                if (res.target)
                                    this.modelStateService.set(JSON.parse(`${res.target?.result}`));
                            };
                        } else {
                            this.modelStateService.set(error);
                        }
                    }
                }
            }));
    }
}