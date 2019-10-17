import { Component, OnDestroy } from "@angular/core";
import { Observable, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, map, takeUntil, filter } from 'rxjs/operators';
import { Model } from '../model/repository.model';
import { Router, NavigationError } from '@angular/router';

@Component({
  selector: "d2HomePage",
  templateUrl: "homePage.component.html"
})
export class HomePageComponent implements OnDestroy {

  ngUnsubscribe = new Subject<void>();
  searchErrorMessage: string = "";

  constructor(private model: Model, private router: Router) { }

  ngOnInit() {
    this.router.events.pipe(
      takeUntil(this.ngUnsubscribe),
      filter(event => event instanceof NavigationError)
    )
      .subscribe(event => {
        let error = event as NavigationError;
        if (error.error.status === 404) {
          this.searchErrorMessage = "No match found with this ID.";
        }
      });
  }
  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
  getHeroesAndAbilitiesList(): string[] {
    return (this.model.getHeroes().map(h => h.LocalizedName + " / Hero")).concat(
      this.model.getAbilities().map(a => a.Name + " / Ability"));
  }

  searchDota = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(200),
      distinctUntilChanged(),
      map(term => {
        term.length < 2 ? []
          : this.getHeroesAndAbilitiesList().filter(x => x.toLowerCase().indexOf(term.toLowerCase()) > -1).slice(0, 10);
        this.searchErrorMessage = "";
      })
    )

  submitEnter(event: any) {
    var item = event.target.value;
    this.submit(item);
  }

  submitButton(item: any) {
    this.submit(item);
  }

  submit(item: any) {
    var numberItem = +item;
    if (isNaN(numberItem)) {
      let stringItem: string = String(item);
      let foundMatches = this.getHeroesAndAbilitiesList().filter(x => x.toLowerCase() == stringItem);
      if (foundMatches.length > 0) {
        let hero = this.model.getHeroes().find(h => h.LocalizedName == stringItem);
        if (hero != null) {
          this.router.navigate(["heroes", hero.HeroId]);
        }
        let ability = this.model.getAbilities().find(a => a.Name == stringItem);
        if (ability != null) {
          this.router.navigate(["abilities", ability.AbilityId]);
        }
      }
      this.searchErrorMessage = `Searched for \"${item}\" but no hero or ability found with that name.`;
    }
    else {
      this.router.navigate(["matches", numberItem]);
    }
  }

  selectedItem(item: any) {
    var itemString = item.item + "";
    var pos = itemString.lastIndexOf(" / ");
    if (pos != -1) {
      let typePos = pos + 3;
      var typeString = itemString.slice(typePos, itemString.length);
      itemString = itemString.slice(0, pos);

      if (typeString == "Ability") {
        let ability = this.model.getAbilities().find(a => a.Name == itemString);
        this.router.navigate(["abilities", ability.AbilityId]);
      }
      else if (typeString == "Hero") {
        let hero = this.model.getHeroes().find(h => h.LocalizedName == itemString);
        this.router.navigate(["heroes", hero.HeroId]);
      }
    }
    else {
      console.log("Selected item was not an expected item");
    }
  }

}