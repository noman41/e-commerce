import { Route } from "@angular/router";
import { AuthGuard } from "../shared/services/auth.guard";
import { OrdersComponent } from "./views/orders/orders.component";

export const SALES_ROUTING: Route = {
    path: '',
    children : [
        {
            path: '',
            component: OrdersComponent,
            canActivate: [AuthGuard]
        },
        {
            path: 'orders',
            component: OrdersComponent,
            canActivate: [AuthGuard]
        }
    ]
}