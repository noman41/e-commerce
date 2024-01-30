import { EventEmitter, Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { LoginModel } from '../models/login.model';
import { RefreshTokenModel } from '../models/refresh-token.model';
import { RestfulService } from './restful.service';
import { tap } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private userKey = 'POSUserIdentity';
  authChanged = new EventEmitter<boolean>();

  constructor(private restfulService: RestfulService, private router: Router) {}

  login(loginData: LoginModel): Observable<any> {
    return this.restfulService.PostRequest("Account/Login", loginData).pipe(
      map((response) => {
        this.SetUserInLocalStorage(response);
      })
    );
  }

  logout(){
    this.RemoveUserFromLocalStorage();
    this.router.navigate(['/login']);
  }

  refreshToken(loginData: RefreshTokenModel, userData: any): Observable<any> {
    return this.restfulService.PostRequest("Account/RefreshToken", loginData).pipe(
      tap((response: any) => {
        userData.accessToken = response.accessToken;
        userData.refreshToken = response.refreshToken;
        this.SetUserInLocalStorage(userData);
      })
    );
  }

  SetUserInLocalStorage(user: any): void {
    localStorage.setItem(this.userKey, JSON.stringify(user));
    this.authChanged.emit(true);
  }

  GetUserFromLocalStorage(): any {
    const userData = localStorage.getItem(this.userKey);
    return userData ? JSON.parse(userData) : null;
  }

  RemoveUserFromLocalStorage(): void {
    localStorage.removeItem(this.userKey);
    this.authChanged.emit(false);
  }
  
  isAuthenticated(): boolean {
    return !!this.GetUserFromLocalStorage();
  }
}
