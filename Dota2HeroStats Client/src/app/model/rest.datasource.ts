import { Injectable, Inject, InjectionToken } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse, HttpRequest } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry, map } from 'rxjs/operators';
import { Hero } from './hero.model';
import { Ability } from './ability.model';
import { HeroAbilityStat } from './heroAbilityStat.model';
import { AbilityDraftMatch } from './abilityDraftMatch.model';


export const REST_URL = new InjectionToken("rest_url");

@Injectable()
export class RestDataSource {

    constructor(private http: HttpClient, @Inject(REST_URL) private url: string) { }

    getLoginData(): Observable<string> {
        return this.sendRequest("GET", `${this.url}/api/admin`) as Observable<string>;
    }

    getHeroData(): Observable<Hero[]> {
        return this.sendRequest("GET", `${this.url}/api/Heroes`) as Observable<Hero[]>;
    }

    getAbilityData(): Observable<Ability[]> {
        return this.sendRequest("GET", `${this.url}/api/Abilities`) as Observable<Ability[]>;
    }

    getHeroAbilityStatData(): Observable<HeroAbilityStat[]> {
        return this.sendRequest("GET", `${this.url}/api/HeroAbilityStats`) as Observable<HeroAbilityStat[]>;
    }

    getAbilityDraftMatch(id: number): Observable<AbilityDraftMatch> {
        return this.sendRequest("GET", `${this.url}/api/AbilityDraftMatches/${id}`) as Observable<AbilityDraftMatch>;
    }

    getAbilityDraftMatchData(): Observable<AbilityDraftMatch[]> {
        return this.sendRequest("GET", `${this.url}/api/AbilityDraftMatches`) as Observable<AbilityDraftMatch[]>;
    }

    updateHero(hero: Hero) {
        return this.sendRequest("PUT", `${this.url}/api/Heroes/${hero.HeroId}`, hero);
    }

    updateAbility(ability: Ability) {
        return this.sendRequest("PUT", `${this.url}/api/Abilities/${ability.AbilityId}`, ability);
    }

    private sendRequest(verb: string, url: string, body?: any): Observable<any> {

        let headers = new HttpHeaders();
        headers = headers.set("Application-Names", ["dota2HeroStats"]);

        return this.http.request<any>(verb, url,
            {
                body: body,
                headers: headers,
                withCredentials: true
            }
        ).pipe(
            retry(1),
            catchError((error: any) => {
                // if (error.status < 400 || error.status === 500) {

                // }
                return throwError(error);
            })
        );
    }
}