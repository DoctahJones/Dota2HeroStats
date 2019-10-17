import { Injectable } from "@angular/core";
import { StaticDataSource } from "./static.datasource";
import { Hero } from "./hero.model";
import { Ability } from "./ability.model";
import { HeroAbilityStat } from "./heroAbilityStat.model";
import { AbilityDraftMatch } from './abilityDraftMatch.model';
import { RestDataSource } from './rest.datasource';
import { throwError, Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

@Injectable()
export class Model {
    private heroes: Hero[] = new Array<Hero>();
    private abilities: Ability[] = new Array<Ability>();
    private heroAbilityStats: HeroAbilityStat[] = new Array<HeroAbilityStat>();
    private abilityDraftMatches: AbilityDraftMatch[] = new Array<AbilityDraftMatch>();

    //switch to StaticDataSource in constructor for local data
    constructor(private dataSource: RestDataSource) {
        this.reloadRestData();
    }

    getHeroes(): Hero[] {
        return this.heroes;
    }

    getHero(id: number): Hero {
        return this.heroes.find(h => h.HeroId == id);
    }

    getAbilities(): Ability[] {
        return this.abilities;
    }

    getAbility(id: number): Ability {
        return this.abilities.find(a => a.AbilityId == id);
    }

    getHeroAbilityStats(heroId?: number): HeroAbilityStat[] {
        return this.heroAbilityStats.filter(has => heroId ? has.HeroId == heroId : true);
    }

    getAbilityDraftMatches(): AbilityDraftMatch[] {
        return this.abilityDraftMatches;
    }

    getAbilityDraftMatch(matchId: number): Observable<any> {
        let localCopy = this.abilityDraftMatches.filter(adm => matchId ? adm.MatchId == matchId : true);
        if (localCopy) {
            return of(localCopy);
        }
        else {
            return this.dataSource.getAbilityDraftMatch(matchId).pipe(
                map(adm => {
                    adm.Players.forEach(p => p.Hero = Object.assign(new Hero(), p.Hero));
                    adm.Players.forEach(p => {
                        let localAbs = new Array<Ability>();
                        p.Abilities.forEach(pa => localAbs.push(Object.assign(new Ability(), pa)))
                        p.Abilities = localAbs;
                    })
                    this.abilityDraftMatches.push(Object.assign(new AbilityDraftMatch(), adm))
                }),
                catchError((error: any) => {
                    console.error("Error Fetching Match: " + matchId, error);
                    return throwError(error);
                }));
        }
    }

    reloadRestData(): Model {
        this.dataSource.getHeroData().subscribe({
            next: h => h.forEach(currH => this.heroes.push(Object.assign(new Hero(), currH)))
        });
        this.dataSource.getAbilityData().subscribe({
            next: a => a.forEach(currA => this.abilities.push(Object.assign(new Ability(), currA)))
        });
        this.dataSource.getHeroAbilityStatData().subscribe({
            next: has => has.forEach(currHas => this.heroAbilityStats.push(Object.assign(new HeroAbilityStat(), currHas)))
        });
        this.dataSource.getAbilityDraftMatchData().subscribe({
            next: matches => matches.forEach(currAdm => {
                //for each match reassign heroes and abilities to correct types
                currAdm.Players.forEach(p => p.Hero = Object.assign(new Hero(), p.Hero));
                currAdm.Players.forEach(p => {
                    let localAbs = new Array<Ability>();
                    p.Abilities.forEach(pa => localAbs.push(Object.assign(new Ability(), pa)))
                    p.Abilities = localAbs;
                });
                this.abilityDraftMatches.push(Object.assign(new AbilityDraftMatch(), currAdm));
            })
        });
        return this;
    }

    saveHero(hero: Hero): Observable<any> {
        return this.dataSource.updateHero(hero).pipe(
            map(h => {
                let index = this.getHeroes().findIndex(item => item.HeroId == hero.HeroId);
                this.heroes.splice(index, 1, hero);
            }),
            catchError((error: any) => {
                console.error("Error saving hero.", error);
                return throwError(error);
            }));
    }

    saveAbility(ability: Ability): Observable<any> {
        return this.dataSource.updateAbility(ability).pipe(
            map(a => {
                let index = this.getAbilities().findIndex(item => item.AbilityId == ability.AbilityId);
                this.abilities.splice(index, 1, ability);
            }),
            catchError((error: any) => {
                console.error("Error saving ability.", error);
                return throwError(error);
            }));
    }

}