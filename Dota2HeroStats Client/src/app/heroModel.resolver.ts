import { Injectable } from '@angular/core';
import { Model } from './model/repository.model';
import { RestDataSource } from './model/rest.datasource';
import { ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { Hero } from './model/hero.model';


@Injectable()
export class HeroModelResolver {
    constructor(private model: Model, private dataSource: RestDataSource) { }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<Hero[]> {
        if(this.model.getHeroes().length > 0)  {
            return of(this.model.getHeroes());
        }
        return this.dataSource.getHeroData();
    }

}
