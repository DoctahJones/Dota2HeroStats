import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { RestAuthService } from '../model/restAuth.service';


@Injectable()
export class AdminResolver {
    constructor(private dataSource: RestAuthService) { }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<string> {
        return this.dataSource.authenticate();
    }
    
}
