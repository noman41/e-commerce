import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError, catchError, switchMap, EMPTY } from 'rxjs';
import { BaseRequestModel } from '../models/base-request.model';
import { AuthService } from './auth.service';
import { RefreshTokenModel } from '../models/refresh-token.model';

@Injectable({
  providedIn: 'root'
})
export class AuthInterceptor implements HttpInterceptor {

  private accessToken: string = "";
  private refreshToken: string = "";
  private user: any;
  constructor(private authService: AuthService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    this.user = this.authService.GetUserFromLocalStorage();
    this.accessToken = this.user?.accessToken;
    this.refreshToken = this.user?.refreshToken;

    if (request.method == 'POST' || request.method == 'PUT') {
      const bodyObj: BaseRequestModel = JSON.parse(request.body);
      if (this.user != undefined && this.user != null) {
        bodyObj.userId = this.user.UserId;
        bodyObj.userEmail = this.user.UserEmail;
      }
      request = request.clone({ body: JSON.stringify(bodyObj) });
    }
    request = request.clone({ setHeaders: { 'Content-Type': 'application/json' } });
    if (this.accessToken && this.refreshToken) {
      request = request.clone({ setHeaders: { 'Authorization': `Bearer ${this.accessToken}` }});
    }
    return next.handle(request).pipe(
      catchError(error => {
        if (error.status === 401 && this.accessToken && this.refreshToken) {
          let refreshTokenModel = new RefreshTokenModel();
          refreshTokenModel.refreshToken = this.refreshToken;
          return this.authService.refreshToken(refreshTokenModel, this.user).pipe(
            switchMap(response => {
              if (response.accessToken){
                request = request.clone({ setHeaders: { 'Authorization': `Bearer ${response.accessToken}` }});
                return next.handle(request);
              }
              this.authService.logout();
              return EMPTY;
            })
          );
        }
        else{
          console.log('error log in else block');
          return throwError(error);
        }
      })
    );
  }
}
