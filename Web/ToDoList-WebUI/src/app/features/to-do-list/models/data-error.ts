export class DataError {
    constructor(status: number, message: string, friendlyMessage: string){
        this.status = status;
        this.message = message;
        this.friendlyMessage = friendlyMessage;
    }
    status: number;
    message: string;
    friendlyMessage: string;
}