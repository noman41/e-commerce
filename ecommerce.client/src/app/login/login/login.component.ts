import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/shared/services/auth.service';
import { LoginModel } from '../../shared/models/login.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  public username = '';
  public password = '';

  constructor(private authService: AuthService, private router: Router) {}

  login(): void {
    let loginData = new LoginModel();
    loginData.userName = this.username;
    loginData.password = this.password;
    this.authService.login(loginData).subscribe(
      (response: any) => {
        this.router.navigate(['/sales']);
      },
      (error) => {
        console.error('Authentication failed:', error);
      }
    );
  }
}