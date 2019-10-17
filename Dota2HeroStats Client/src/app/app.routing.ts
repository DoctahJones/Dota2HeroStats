import { Routes, RouterModule } from "@angular/router";
import { HeroTableComponent } from './core/heroTable.component';
import { HeroAbilityStatListComponent } from './core/heroAbilityStatList.component';
import { HomePageComponent } from './core/homePage.component';
import { HeroPageComponent } from './core/heroPage.component';
import { AbilityPageComponent } from './core/abilityPage.component';
import { AbilityDraftMatchListComponent } from './core/abilityDraftMatchList.component';
import { AbilityDraftMatchPageComponent } from './core/abilityDraftMatchPage.component';
import { HeroModelResolver } from './heroModel.resolver';
import { AbilityModelResolver } from './abilityModel.resolver';
import { AbilityDraftMatchModelResolver } from './abilityDraftMatch.resolver';



const routes: Routes = [
    {
        path: "abilities/:id", component: AbilityPageComponent,
        resolve: {
            abilityModel: AbilityModelResolver
        }
    },
    { path: "abilities", component: HeroAbilityStatListComponent },
    {
        path: "heroes/:id", component: HeroPageComponent,
        resolve: {
            heroModel: HeroModelResolver
        }
    },
    { path: "heroes", component: HeroTableComponent },
    {
        path: "matches/:id", component: AbilityDraftMatchPageComponent,
        resolve: {
            abilityDraftMatchModel: AbilityDraftMatchModelResolver
        }
    },
    { path: "matches", component: AbilityDraftMatchListComponent },
    {
        path: 'admin',
        loadChildren: () => import('./admin/admin.module').then(mod => mod.AdminModule)
    },
    { path: "", component: HomePageComponent }]


export const routing = RouterModule.forRoot(routes);