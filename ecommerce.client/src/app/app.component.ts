import { Component, OnInit } from '@angular/core';
import { AuthService } from './shared/services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  isAuthenticated = false;
  
  constructor(private authService: AuthService) {
    this.isAuthenticated = authService.isAuthenticated();
  }

  ngOnInit(){
    this.authService.authChanged.subscribe(async (data) => {
      this.isAuthenticated = data;
    })
  }

  title = 'Point of Sale';
}
