import { NgModule } from '@angular/core';
import { LoginComponent } from './login/login.component';
import { SharedModule } from '../shared/shared.module';
import { RouterModule } from '@angular/router';
import { LOGIN_ROUTING } from './login.route';

@NgModule({
  declarations: [
    LoginComponent
  ],
  imports: [
    RouterModule.forChild([LOGIN_ROUTING]),
    SharedModule
  ]
})
export class LoginModule { }
