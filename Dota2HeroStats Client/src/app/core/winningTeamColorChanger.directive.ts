import { Directive, ElementRef, Input, Renderer2 } from '@angular/core';

@Directive({
  selector: '[d2-winningTeamColorChanger]'
})
export class WinningTeamColorChangerDirective {

  constructor(private element: ElementRef, private render: Renderer2) { }

  @Input("d2-winningTeamColorChanger")
  radiantWin: boolean;

  ngOnInit() {
    this.render.setStyle(this.element.nativeElement, (this.radiantWin ? "colorRadiant" : "colorDire"), true);
  }

}
