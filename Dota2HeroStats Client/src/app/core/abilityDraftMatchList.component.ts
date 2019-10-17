import { Component, Inject, Input } from "@angular/core";
import { Model } from "../model/repository.model";
import { Hero } from "../model/hero.model";
import { AbilityDraftMatch } from '../model/abilityDraftMatch.model';

@Component({
    selector: "d2AbilityDraftMatchList",
    templateUrl: "abilityDraftMatchList.component.html"
})
export class AbilityDraftMatchListComponent {

    constructor(private model: Model) {
    }

    getMatches(): AbilityDraftMatch[] {
        return this.model.getAbilityDraftMatches();
    }

    getMatchRadiantHeroes(match: AbilityDraftMatch): Hero[] {
        return match.Players.filter(p => p.PlayerSlot < 5).map(p => p.Hero);
    }

    getMatchDireHeroes(match: AbilityDraftMatch): Hero[] {
        return match.Players.filter(p => p.PlayerSlot > 5).map(p => p.Hero);
    }
}