import { Component, Inject } from "@angular/core";
import { Model } from "../model/repository.model";
import { ActivatedRoute, Router } from '@angular/router';
import { REST_URL } from '../model/rest.datasource';
import { AbilityDraftMatch } from '../model/abilityDraftMatch.model';

@Component({
    selector: "d2MatchListAdmin",
    templateUrl: "matchListAdmin.component.html"
})
export class MatchListAdminComponent {

    loggedIn: boolean = false;

    constructor(private model: Model, private activeRoute: ActivatedRoute, private router: Router, @Inject(REST_URL) private url: string) {
    }

    getMatches(): AbilityDraftMatch[] {
        return this.model.getAbilityDraftMatches();
    }

    goToMatch(item: any) {
        let stringItem: string = String(item);
        if (stringItem != "") {
            this.router.navigateByUrl('/admin/matches/' + stringItem);
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