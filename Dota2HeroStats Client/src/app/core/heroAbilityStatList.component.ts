import { Component, Input } from "@angular/core";
import { Model } from "../model/repository.model";
import { Ability } from "../model/ability.model";
import { HeroAbilityStat } from "../model/heroAbilityStat.model";
import { Hero } from "../model/hero.model";

@Component({
    selector: "d2HeroAbilityStatList",
    templateUrl: "heroAbilityStatList.component.html"
})
export class HeroAbilityStatListComponent {
    descending: boolean = true;

    @Input() hero: Hero;
    @Input() ability: Ability;

    constructor(private model: Model) { }

    getHeroAbilityStats(): HeroAbilityStat[] {
        var stats = this.hero ? this.model.getHeroAbilityStats(this.hero.HeroId) : this.model.getHeroAbilityStats();
        stats = stats.filter(s => this.ability ? s.AbilityId == this.ability.AbilityId : true);

        if (this.descending) {
            return stats.sort((a, b) => 0 - (+this.getWinPercentage(a) > +this.getWinPercentage(b) ? 1 : -1));
        }
        else {
            return stats.sort((a, b) => 0 - (+this.getWinPercentage(a) > +this.getWinPercentage(b) ? -1 : 1));
        }
    }

    getHeroFromId(heroId: number): Hero {
        return this.model.getHero(heroId);
    }
    getAbilityFromId(abilityId: number): Ability {
        return this.model.getAbility(abilityId);
    }
    getWinPercentage(stat: HeroAbilityStat): string {
        if (stat.Matches > 0) {
            return ((stat.Wins / stat.Matches) * 100).toFixed(1);
        }
    }
}