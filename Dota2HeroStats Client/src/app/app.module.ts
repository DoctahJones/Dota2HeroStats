import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { NavbarModule } from './navbar/navbar.module';
import { CoreModule } from './core/core.module';
import { routing } from './app.routing';
import { RouterModule } from '@angular/router';
import { HeroModelResolver } from './heroModel.resolver';
import { AbilityModelResolver } from './abilityModel.resolver';
import { AbilityDraftMatchModelResolver } from './abilityDraftMatch.resolver';
import { AdminModule } from './admin/admin.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule, NavbarModule, CoreModule, AdminModule, FormsModule, ReactiveFormsModule, RouterModule, routing
  ],
  providers: [HeroModelResolver, AbilityModelResolver, AbilityDraftMatchModelResolver],
  bootstrap: [AppComponent]
})
export class AppModule { }
