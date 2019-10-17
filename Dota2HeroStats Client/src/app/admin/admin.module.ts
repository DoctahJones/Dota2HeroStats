import { NgModule } from "@angular/core";
import { ModelModule } from "../model/model.module";
import { RouterModule } from '@angular/router';
import { HomeAdminComponent } from './homeAdmin.component';
import { HeroAdminComponent } from './heroAdmin.component';
import { HeroListAdminComponent } from './heroListAdmin.component';
import { AbilityAdminComponent } from './abilityAdmin.component';
import { MatchListAdminComponent } from './matchListAdmin.component';
import { MatchAdminComponent } from './matchAdmin.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AbilityListAdminComponent } from './abilityListAdmin.component';
import { AdminRoutingModule } from './admin.routing';
import { BrowserModule } from '@angular/platform-browser';
import { AdminResolver } from './admin.resolver';

@NgModule({
    imports: [BrowserModule, ModelModule, RouterModule, FormsModule, ReactiveFormsModule, AdminRoutingModule],
    declarations: [HomeAdminComponent, HeroListAdminComponent, HeroAdminComponent,
        MatchListAdminComponent, MatchAdminComponent, AbilityListAdminComponent, AbilityAdminComponent],
    exports: [ModelModule, HomeAdminComponent, HeroListAdminComponent, HeroAdminComponent,
        MatchListAdminComponent, MatchAdminComponent, AbilityListAdminComponent, AbilityAdminComponent],
    providers: [AdminResolver]

})
export class AdminModule { }