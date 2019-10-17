import { Component } from "@angular/core";
import { Model } from "../model/repository.model";
import { Hero } from "../model/hero.model";

@Component({
    selector: "d2HeroTable",
    templateUrl: "heroTable.component.html"
})
export class HeroTableComponent {
    constructor(private model: Model) { }

    getHeroes(attr: string): Hero[] {
        //no heroes loaded yet
        if(this.model.getHeroes() == null)  {
            return new Array<Hero>();
        }
        else {
            return this.model.getHeroes().filter(h => attr == null || h.PrimaryAttr == attr);
        }
        
    }

    

}