import { Component, Inject } from "@angular/core";
import { Model } from "../model/repository.model";
import { ActivatedRoute, Router } from '@angular/router';
import { REST_URL } from '../model/rest.datasource';
import { Hero } from '../model/hero.model';

@Component({
    selector: "d2HeroAdmin",
    templateUrl: "heroAdmin.component.html"
})
export class HeroAdminComponent {

    loggedIn: boolean = false;
    hero: Hero = new Hero();

    formSubmitted: boolean = false;
    submitError: boolean = false;

    availableRoles: string[] = new Array<string>();

    constructor(private model: Model, private activeRoute: ActivatedRoute, private router: Router, @Inject(REST_URL) private url: string) {
        activeRoute.params.subscribe(params => {
            let id = activeRoute.snapshot.params["id"];
            if (id != null) {
                let localHero = model.getHero(id);
                if (localHero != null) {
                    Object.assign(this.hero, localHero);
                }
            }
        })
    }

    ngOnInit() {
        this.activeRoute.data.subscribe({
            next: data => {
                if (data != null && data.steamAuthentication != null && data.steamAuthentication != "") {
                    this.loggedIn = true;
                }

                let localHero = data.heroModel.find(h => h.HeroId == this.activeRoute.snapshot.params["id"]);
                if (localHero != null) {
                    Object.assign(this.hero, localHero);
                }
                this.updateAvailableRoles();
            },
            error: e => {
                console.error(e);
            }
        });
    }

    updateAvailableRoles() {
        let roles: string[] = new Array<string>();
        //get every role from every hero and add it to a list of roles if it isn't already in there.
        this.model.getHeroes().map(h => h.Roles).forEach(roleSet => {
            roleSet.forEach(role => {
                if (!roles.includes(role)) {
                    roles.push(role);
                }
            });
        });
        //remove roles already on the current hero.
        this.hero.Roles.forEach(role => {
            var index: number = roles.indexOf(role, 0);
            if (index > -1) {
                roles.splice(index, 1);
            }
        });
        this.availableRoles = roles.sort();
    }

    addRole(item: any) {
        let stringItem: string = String(item);
        if (stringItem != "") {
            var newArray = Array.from(this.hero.Roles);
            newArray.push(stringItem);
            this.hero.Roles = newArray;
            this.updateAvailableRoles();
        }
    }

    addNewRole(item: any) {
        let stringItem: string = String(item);
        if (stringItem != "") {
            var newArray = Array.from(this.hero.Roles);
            newArray.push(stringItem);
            this.hero.Roles = newArray;
            var index: number = this.availableRoles.indexOf(stringItem, 0);
            if (index > -1) {
                this.availableRoles.splice(index, 1);
            } else {
                this.availableRoles.push(stringItem);
            }
        }
    }

    removeRole(item: any) {
        let stringItem: string = String(item);
        if (stringItem != "") {
            var newArray = Array.from(this.hero.Roles);
            var index: number = newArray.indexOf(stringItem, 0);
            if (index > -1) {
                newArray.splice(index, 1);
            }
            this.hero.Roles = newArray;
            this.updateAvailableRoles();
        }
    }

    onSubmit() {
        this.submitError = false;
        this.formSubmitted = true;

        this.model.saveHero(this.hero).subscribe({
            next: () => {
                this.router.navigateByUrl('/admin/heroes');
            },
            error: err => {
                this.submitError = true;
            }
        });
        this.formSubmitted = false;
    }

}