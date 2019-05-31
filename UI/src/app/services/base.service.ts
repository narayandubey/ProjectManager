import { Response } from '@angular/http';
import { Observable } from 'rxjs';
import { ServiceError } from '../models/serviceerror';
export class BaseService {
    constructor() { }
    protected extractData(res: Response) {
        const body = res.json();
        if (body.Status === 'success') {
            return body.Data;
        } else if (body.Status === 'fail') {
            throw new ServiceError(body.Message, body.Data, 'fail');
        } else if (body.Status === 'error') {
            throw new ServiceError(body.Message, body.Data);
        } else {
            throw new ServiceError('Invalid JSend Response Status [' + body.status + ']');
        }
    }
    public baseurl(): string {
        return '';
    }

    protected handleError(error: any) {
        if (error instanceof ServiceError) {
            return Observable.throw(error);
        } else {
            const errMsg = (error.message) ? error.message : error.status ? `${error.status} - ${error.statusText}` : 'Server error';
            return Observable.throw(new ServiceError(errMsg));
        }
    }
}