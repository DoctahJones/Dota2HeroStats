import { Component } from "@angular/core";
import { ActivatedRoute } from '@angular/router';
import { Model } from '../model/repository.model';
import { Ability } from '../model/ability.model';
@Component({
    selector: "d2AbilityPage",
    templateUrl: "abilityPage.component.html"
})
export class AbilityPageComponent {

    ability: Ability = new Ability();

    constructor(private model: Model, private activeRoute: ActivatedRoute) {
    }

    ngOnInit() {
        this.activeRoute.data.subscribe((data: { abilityModel: Ability[] }) => {
            Object.assign(this.ability, data.abilityModel.find(a => a.AbilityId == this.activeRoute.snapshot.params["id"]));
          });
      }


}