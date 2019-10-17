import { Component, Input, SimpleChanges } from "@angular/core";
import { Model } from "../model/repository.model";
import { Hero } from "../model/hero.model";
import { Ability } from "../model/ability.model";

@Component({
    selector: "d2Ability",
    templateUrl: "ability.component.html"
})
export class AbilityComponent {

    @Input() ability: Ability;

    constructor(private model: Model) {
    }

    getHeroFromId(id: number): Hero {
        return this.model.getHero(id);
    }

}