import { NgModule } from "@angular/core";
import { Model } from "./repository.model";
import { StaticDataSource } from "./static.datasource";
import { RestDataSource, REST_URL } from './rest.datasource';
import { HttpClientModule } from '@angular/common/http';
import { RestAuthService } from './restAuth.service';
@NgModule({
    imports: [HttpClientModule],
    providers: [Model, StaticDataSource, RestDataSource, RestAuthService,
        { provide: REST_URL, useValue: `http://localhost:2047` }//TODO set rest url location.
    ]
})
export class ModelModule { }