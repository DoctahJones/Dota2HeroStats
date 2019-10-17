import { Injectable, InjectionToken } from '@angular/core';
import { RestDataSource } from './rest.datasource';
import { Observable } from 'rxjs';

@Injectable()
export class RestAuthService {

    constructor(private dataSource: RestDataSource) {
    }

    authenticate() : Observable<string> {
        return this.dataSource.getLoginData();
    }

}