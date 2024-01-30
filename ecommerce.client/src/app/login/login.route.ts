import { Route } from "@angular/router";
import { LoginComponent } from "./login/login.component";

export const LOGIN_ROUTING: Route = {
    path: '',
    children : [
        {path:'login', component: LoginComponent, title: 'Login'}
    ]
}