import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HeroAdminComponent } from './heroAdmin.component';
import { HeroModelResolver } from '../heroModel.resolver';
import { HeroListAdminComponent } from './heroListAdmin.component';
import { AbilityAdminComponent } from './abilityAdmin.component';
import { AbilityModelResolver } from '../abilityModel.resolver';
import { AbilityListAdminComponent } from './abilityListAdmin.component';
import { MatchAdminComponent } from './matchAdmin.component';
import { AbilityDraftMatchModelResolver } from '../abilityDraftMatch.resolver';
import { MatchListAdminComponent } from './matchListAdmin.component';
import { HomeAdminComponent } from './homeAdmin.component';
import { AdminResolver } from './admin.resolver';

const routes: Routes = [
    {
        path: "admin/heroes/:id", component: HeroAdminComponent,
        resolve: {
            steamAuthentication: AdminResolver,
            heroModel: HeroModelResolver
        }
    },
    {
        path: "admin/heroes", component: HeroListAdminComponent,
        resolve: {
            steamAuthentication: AdminResolver
        }
    },
    {
        path: "admin/abilities/:id", component: AbilityAdminComponent,
        resolve: {
            steamAuthentication: AdminResolver,
            abilityModel: AbilityModelResolver
        }
    },
    {
        path: "admin/abilities", component: AbilityListAdminComponent,
        resolve: {
            steamAuthentication: AdminResolver
        }
    },
    {
        path: "admin/matches/:id", component: MatchAdminComponent,
        resolve: {
            steamAuthentication: AdminResolver,
            abilityDraftMatchModel: AbilityDraftMatchModelResolver
        }
    },
    {
        path: "admin/matches", component: MatchListAdminComponent,
        resolve: {
            steamAuthentication: AdminResolver
        }
    },
    {
        path: "admin", component: HomeAdminComponent,
        resolve: {
            steamAuthentication: AdminResolver
        }
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AdminRoutingModule { }