import { Component, Inject } from "@angular/core";
import { Model } from "../model/repository.model";
import { ActivatedRoute, Router } from '@angular/router';
import { REST_URL } from '../model/rest.datasource';
import { Hero } from '../model/hero.model';

@Component({
    selector: "d2HeroListAdmin",
    templateUrl: "heroListAdmin.component.html"
})
export class HeroListAdminComponent {

    loggedIn: boolean = false;

    constructor(private model: Model, private activeRoute: ActivatedRoute, private router: Router, @Inject(REST_URL) private url: string) {
    }

    getHeroes(): Hero[] {
        if (this.model.getHeroes() == null) {
            return new Array<Hero>();
        }
        else {
            return this.model.getHeroes().sort((h1, h2) => (h1.LocalizedName > h2.LocalizedName) ? 1 : ((h2.LocalizedName > h1.LocalizedName) ? -1 : 0));
        }
    }

    goToHero(item: any) {
        let stringItem: string = String(item);
        if (stringItem != "") {
            this.router.navigateByUrl('/admin/heroes/' + stringItem);
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