import { Component, Inject } from "@angular/core";
import { Model } from "../model/repository.model";
import { ActivatedRoute, Router } from '@angular/router';
import { REST_URL } from '../model/rest.datasource';
import { Ability } from '../model/ability.model';
import { Hero } from '../model/hero.model';

@Component({
    selector: "d2AbilityAdmin",
    templateUrl: "abilityAdmin.component.html"
})
export class AbilityAdminComponent {

    loggedIn: boolean = false;
    ability: Ability = new Ability();

    formSubmitted: boolean = false;
    submitError: boolean = false;

    constructor(private model: Model, private activeRoute: ActivatedRoute, private router: Router, @Inject(REST_URL) private url: string) {
    }

    get HeroList(): string[] {
        return this.model.getHeroes().map(h => h.LocalizedName);
    }

    setOwner(item: any) {
        let stringItem: string = String(item);
        if (stringItem != "") {
            var hero: Hero = this.model.getHeroes().find(h => h.LocalizedName == stringItem);
            if (hero != null) {
                this.ability.OriginalAbilityOwnerId = hero.HeroId;
            }
        }
    }

    ngOnInit() {
        this.activeRoute.data.subscribe({
            next: data => {
                if (data != null && data.steamAuthentication != null && data.steamAuthentication != "") {
                    this.loggedIn = true;
                }
                Object.assign(this.ability, data.abilityModel.find(a => a.AbilityId == this.activeRoute.snapshot.params["id"]));
            },
            error: e => {
                console.error(e);
            }
        });
    }

    onSubmit() {
        this.submitError = false;
        this.formSubmitted = true;

        this.model.saveAbility(this.ability).subscribe({
            next: a => {
                this.router.navigateByUrl('/admin');
            },
            error: err => {
                this.submitError = true;

            }
        });
        this.formSubmitted = false;
    }

}