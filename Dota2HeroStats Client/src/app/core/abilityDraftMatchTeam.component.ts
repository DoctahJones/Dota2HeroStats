import { Component, Inject, Input } from "@angular/core";
import { Model } from "../model/repository.model";
import { Player } from '../model/player.model';

@Component({
    selector: "d2AbilityDraftMatchTeam",
    templateUrl: "abilityDraftMatchTeam.component.html"
})
export class AbilityDraftMatchTeamComponent {


    @Input() players: Player[];

    constructor(private model: Model) {
    }



    getServerRegionFromCluster(cluster: number): string {
        if (cluster > 110 && cluster << 115) {
            return "US West";
        }
        else if (cluster > 120 && cluster < 125) {
            return "US East";
        }
        else if (cluster > 130 && cluster < 137) {
            return "EU West";
        }
        else if (cluster > 140 && cluster < 144) {
            return "South Korea";
        }
        else if (cluster > 150 && cluster < 160) {
            return "SEA";
        }
        else if (cluster > 170 && cluster < 175) {
            return "Australia";
        }
        else if (cluster > 180 && cluster < 185) {
            return "Russia";
        }
        else if (cluster > 190 && cluster < 195) {
            return "EU East";
        }
        else if (cluster > 199 && cluster < 205) {
            return "South America";
        }
        else if (cluster > 210 && cluster < 215) {
            return "South Africa";
        }
        else if (cluster > 220 && cluster < 235) {
            return "China";
        }
        else {
            return "Unknown Region"
        }
    }

    getPatchStringFromPatchNumber(patchNumber: number): string {
        if (patchNumber == 40) {
            return "Patch 7.21";
        }
        else {
            return "Unknown patch"
        }
    }

    getSkillLevelFromNumber(skillLevel: number): string {
        if (skillLevel == null || 0) {
            return "Normal Skill";
        } else if (skillLevel == 2) {
            return "High Skill";
        } else if (skillLevel == 3) {
            return "Very High Skill";
        }
        else {
            return "Unknown Skill";
        }
    }

}