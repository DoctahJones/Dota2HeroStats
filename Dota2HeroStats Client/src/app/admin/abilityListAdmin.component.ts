import { Component, Inject } from "@angular/core";
import { Model } from "../model/repository.model";
import { ActivatedRoute, Router } from '@angular/router';
import { REST_URL } from '../model/rest.datasource';
import { Ability } from '../model/ability.model';

@Component({
    selector: "d2AbilityListAdmin",
    templateUrl: "abilityListAdmin.component.html"
})
export class AbilityListAdminComponent {

    loggedIn: boolean = false;

    constructor(private model: Model, private activeRoute: ActivatedRoute, private router: Router, @Inject(REST_URL) private url: string) {
    }

    getAbilities(): Ability[] {
        if (this.model.getAbilities() == null) {
            return new Array<Ability>();
        }
        else {
            return this.model.getAbilities().sort((a1, a2) => a1.AbilityId - a2.AbilityId);
        }
    }

    goToAbility(item: any) {
        let stringItem: string = String(item);
        if (stringItem != "") {
            this.router.navigateByUrl('/admin/abilities/' + stringItem);
        }
    }

    ngOnInit() {
        this.activeRoute.data.subscribe((data: { steamAuthentication: string }) => {
            if (data != null && data.steamAuthentication != null && data.steamAuthentication != "") {
                this.loggedIn = true;
            }
        });
    }

}