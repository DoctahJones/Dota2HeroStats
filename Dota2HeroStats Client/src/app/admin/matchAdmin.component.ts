import { Component, Inject } from "@angular/core";
import { Model } from "../model/repository.model";
import { ActivatedRoute } from '@angular/router';
import { REST_URL } from '../model/rest.datasource';
import { AbilityDraftMatch } from '../model/abilityDraftMatch.model';
import { Ability } from '../model/ability.model';
import { Hero } from '../model/hero.model';

@Component({
    selector: "d2MatchAdmin",
    templateUrl: "matchAdmin.component.html"
})
export class MatchAdminComponent {

    loggedIn: boolean = false;
    match: AbilityDraftMatch = new AbilityDraftMatch();

    constructor(private model: Model, private activeRoute: ActivatedRoute, @Inject(REST_URL) private url: string) {
    }



    ngOnInit() {
        this.activeRoute.data.subscribe((data: { steamAuthentication: string, abilityDraftMatchModel: AbilityDraftMatch }) => {
            if (data != null && data.steamAuthentication != null && data.steamAuthentication != "") {
                this.loggedIn = true;
            }

            let locMatch = data.abilityDraftMatchModel;
            locMatch.Players.forEach(p => p.Hero = Object.assign(new Hero(), p.Hero));
            locMatch.Players.forEach(p => {
                let localAbs = new Array<Ability>();
                p.Abilities.forEach(pa => localAbs.push(Object.assign(new Ability(), pa)))
                p.Abilities = localAbs;
            })
            Object.assign(this.match, locMatch);
        });
    }

}