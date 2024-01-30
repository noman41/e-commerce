import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { SALES_ROUTING } from './sales.route';
import { SalesHomeComponent } from './Views/sales-home/sales-home.component';
import { SalesService } from './services/sales.service';
import { OrdersComponent } from './views/orders/orders.component';
import { OrderListComponent } from './views/orders/order-list/order-list.component';
import { OrderAddEditComponent } from './views/orders/order-add-edit/order-add-edit.component';

@NgModule({
  declarations: [
    SalesHomeComponent,
    OrdersComponent,
    OrderListComponent,
    OrderAddEditComponent
  ],
  imports: [
    RouterModule.forChild([SALES_ROUTING]),
    CommonModule
  ],
  providers:[
    SalesService
  ]
})
export class SalesModule { }
