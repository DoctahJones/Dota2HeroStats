import { Component } from "@angular/core";
import { Hero } from '../model/hero.model';
import { ActivatedRoute } from '@angular/router';
import { Model } from '../model/repository.model';
@Component({
    selector: "d2HeroPage",
    templateUrl: "heroPage.component.html"
})
export class HeroPageComponent {

    hero: Hero = new Hero();
    loaded: boolean = false;
    errorMessage: string = "";

    constructor(private model: Model, private activeRoute: ActivatedRoute) {
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
                let localHero = data.heroModel.find(h => h.HeroId == this.activeRoute.snapshot.params["id"]);
                if (localHero != null) {
                    Object.assign(this.hero, localHero);
                }
                this.loaded = true;
            },
            error: e => {
                this.loaded = true;
                this.errorMessage = e;
                console.log(e);
            }
         });
    }

}