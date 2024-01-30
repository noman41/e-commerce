export class BaseRequestModel {
    public userId: string;
    public userEmail: string;
  
    constructor() {
      this.userId = "";
      this.userEmail = ""
    }
}