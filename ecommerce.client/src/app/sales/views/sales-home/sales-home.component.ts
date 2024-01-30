import { Component } from '@angular/core';
import { SalesService } from '../../services/sales.service';

@Component({
  selector: 'app-sales-home',
  templateUrl: './sales-home.component.html',
  styleUrls: ['./sales-home.component.css']
})
export class SalesHomeComponent {
  
  public product: string = '';

  constructor(private salesService: SalesService) {}
  
  ngOnInit(): void {
    this.salesService.GetAllProducts().subscribe(
      (response: string) => {
        this.product = response;
    });
  }
}
