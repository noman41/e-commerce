import { BaseRequestModel } from "src/app/shared/models/base-request.model";

export class LoginModel extends BaseRequestModel {
    public userName: string;
    public password: string;
    constructor(){
        super();
        this.userName = "";
        this.password = ""
    }
}