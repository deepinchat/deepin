import { ErrorHandler, Injectable } from "@angular/core";

@Injectable()
export class GlobalErrorHandler implements ErrorHandler {

    constructor() {
    }

    handleError(error: any) {
        console.error('An error occurred:', error);
    }

}
