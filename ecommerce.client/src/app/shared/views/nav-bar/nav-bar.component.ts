import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent {
  isSalesSubmenuOpen = false;

  constructor(private authService: AuthService){}

  ngOnInit() {

  }

  openSalesSubmenu() {
    this.isSalesSubmenuOpen = true;
  }

  closeSalesSubmenu() {
    this.isSalesSubmenuOpen = false;
  }

  logout(){
    this.authService.logout();
  }
}
