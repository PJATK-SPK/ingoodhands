import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthService } from './auth.service';
import { MessageService } from 'primeng/api';
import { Router } from '@angular/router';

@Injectable({ providedIn: 'root' })
export class HttpErrorInterceptor implements HttpInterceptor {

    constructor(
        private readonly msg: MessageService,
        private readonly authService: AuthService,
        private readonly router: Router,
    ) { }

    public intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request)
            .pipe(
                catchError((error: HttpErrorResponse) => {
                    this.showMessage(error);
                    return throwError(() => error);
                })
            )
    }

    private showMessage(err: HttpErrorResponse): void {
        let title = '';
        let texts: string[] = [];
        let show = false;
        let severity = 'error';

        if (err.status === 0) {
            show = true;
            title = 'Connection error';
            texts = ['The server is not responding. Please try again later.'];
        }
        else if (err.status === 500) {
            show = true;
            title = 'Server error';
            texts = err.error.message.split('\n');
        }
        else if (err.status === 400) {
            show = true;
            title = 'Data error';
            texts = err.error.message.split('\n');
            severity = 'warn';
        }
        else if (err.status === 403) {
            show = true;
            title = 'Unauthorized';
            texts = ['You are not authorized to access this page.'];
            this.authService.logout();
        }
        else if (err.status === 401) {
            show = true;
            title = 'Unauthenticated';
            texts = ['You are not authenticated to access this page.'];
            this.authService.logout();
            this.router.navigateByUrl('/');
        }

        if (show) {
            texts.filter(c => c).forEach(text => {
                this.msg.add({ severity, summary: title, detail: text, life: 10000 });
            });
        }
    }
}