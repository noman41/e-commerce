import { Injectable } from "@angular/core";
import { map, Observable } from "rxjs";
import { BaseRequestModel } from "src/app/shared/models/base-request.model";
import { RestfulService } from "src/app/shared/services/restful.service";

@Injectable({
    providedIn: 'root',
})

export class SalesService {

    constructor(private restfulService: RestfulService) {}

    public GetAllProducts(): Observable<string> {
        return this.restfulService.PostRequest('Product/GetAllProducts', new BaseRequestModel()).pipe(
          map((data: any) => {
            return data;
       }));
    }
}