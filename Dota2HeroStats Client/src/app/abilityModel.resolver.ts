import { Injectable } from '@angular/core';
import { Model } from './model/repository.model';
import { RestDataSource } from './model/rest.datasource';
import { ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { Ability } from './model/ability.model';


@Injectable()
export class AbilityModelResolver {
    constructor(private model: Model, private dataSource: RestDataSource) { }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<Ability[]> {
        if(this.model.getAbilities().length > 0)  {
            return of(this.model.getAbilities());
        }
        return this.dataSource.getAbilityData();
    }
}
