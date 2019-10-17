import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LocationStrategy } from '@angular/common';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'dota2stats';

  admin: boolean = false;

  constructor(private router: Router, private url: LocationStrategy) {
  }

  ngOnInit() {
    this.router.events.subscribe(event => {
      if (this.url.path().includes('/admin')) {
        this.admin = true;
      }
      else {
        this.admin = false;
      }
    });

  }


}
