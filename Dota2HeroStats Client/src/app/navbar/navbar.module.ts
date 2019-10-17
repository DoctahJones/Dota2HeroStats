import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NavBarComponent } from './navbar.component';
import { RouterModule } from '@angular/router';

@NgModule({
    declarations: [NavBarComponent],
    imports: [BrowserModule, RouterModule],
    exports: [NavBarComponent],
    providers: []
})
export class NavbarModule { }
