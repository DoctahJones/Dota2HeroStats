import { NgModule } from "@angular/core";
import { ModelModule } from "../model/model.module";
import { HeroTableComponent } from "./heroTable.component";
import { HeroComponent } from "./hero.component";
import { HeroAbilityStatListComponent } from "./heroAbilityStatList.component";
import { PercentColorChangerDirective } from './percentColorChanger.directive';
import { HomePageComponent } from './homePage.component';
import { NgbTypeaheadModule } from '@ng-bootstrap/ng-bootstrap'
import { HeroPageComponent } from './heroPage.component';
import { RouterModule } from '@angular/router';
import { AbilityComponent } from './ability.component';
import { AbilityPageComponent } from './abilityPage.component';
import { AbilityDraftMatchListComponent } from './abilityDraftMatchList.component';
import { D2SecondsToHoursMinutesSecondsPipe } from './secondsToHoursMinutesSeconds.pipe';
import { AbilityDraftMatchPageComponent } from './abilityDraftMatchPage.component';
import { WinningTeamColorChangerDirective } from './winningTeamColorChanger.directive';
import { AbilityDraftMatchTeamComponent } from './abilityDraftMatchTeam.component';
import { BrowserModule } from '@angular/platform-browser';

@NgModule({
    imports: [BrowserModule, ModelModule, RouterModule, NgbTypeaheadModule],
    declarations: [HeroTableComponent, HeroComponent, AbilityComponent, HeroAbilityStatListComponent, HomePageComponent,
        HeroPageComponent, AbilityPageComponent, AbilityDraftMatchListComponent, AbilityDraftMatchTeamComponent,
        AbilityDraftMatchPageComponent,
        PercentColorChangerDirective, WinningTeamColorChangerDirective,
        D2SecondsToHoursMinutesSecondsPipe],
    exports: [ModelModule, HeroTableComponent, HeroComponent, HeroAbilityStatListComponent, AbilityDraftMatchListComponent,
        AbilityDraftMatchPageComponent]

})
export class CoreModule { }