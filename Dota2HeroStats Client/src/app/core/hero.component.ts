import { Component, Input, SimpleChanges } from "@angular/core";
import { Model } from "../model/repository.model";
import { Hero } from "../model/hero.model";
import { Ability } from "../model/ability.model";

@Component({
    selector: "d2Hero",
    templateUrl: "hero.component.html",
    styleUrls: ["hero.component.css"]
})
export class HeroComponent {

    @Input() hero: Hero;
    
    constructor(private model: Model) {
    }

    getHeroOriginalAbilities(): Ability[] {
        return this.model.getAbilities().filter(a => a.OriginalAbilityOwnerId == this.hero.HeroId)
            .sort((a, b) => { return a.AbilityId - b.AbilityId });
    }

    
}