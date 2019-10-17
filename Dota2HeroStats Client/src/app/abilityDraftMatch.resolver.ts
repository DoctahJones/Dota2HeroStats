import { Injectable } from '@angular/core';
import { RestDataSource } from './model/rest.datasource';
import { ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';


@Injectable()
export class AbilityDraftMatchModelResolver {
    constructor( private dataSource: RestDataSource) { }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<any> {
        return this.dataSource.getAbilityDraftMatch(+route.paramMap.get('id')) as Observable<any>;
    }
}
