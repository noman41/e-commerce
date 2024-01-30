import { BaseRequestModel } from "src/app/shared/models/base-request.model";

export class RefreshTokenModel extends BaseRequestModel {
    public refreshToken: string;
    constructor(){
        super();
        this.refreshToken = "";
    }
}