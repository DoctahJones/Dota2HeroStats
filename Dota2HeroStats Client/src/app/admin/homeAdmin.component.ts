import { Component, Inject } from "@angular/core";
import { ActivatedRoute} from '@angular/router';
import { REST_URL} from '../model/rest.datasource';

@Component({
    selector: "d2HomeAdmin",
    templateUrl: "homeAdmin.component.html"
})
export class HomeAdminComponent {

    loggedIn: boolean = false;

    constructor(private activeRoute: ActivatedRoute, @Inject(REST_URL) private url: string) {
    }

    getLoginUrl() {
        let encodedString = this.url + "/account/login/" + "?returnUrl=" + encodeURIComponent(location.href);
        return encodedString;
    }


    ngOnInit() {
        this.activeRoute.data.subscribe((data: { steamAuthentication: string }) => {
            if(data != null && data.steamAuthentication != null && data.steamAuthentication != "")  {
                this.loggedIn = true;
            }
          });
      }

}