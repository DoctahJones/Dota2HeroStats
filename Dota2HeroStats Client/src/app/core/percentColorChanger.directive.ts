import { Directive, ElementRef, Input, Renderer2 } from '@angular/core';

@Directive({
  selector: '[d2-percentColorChanger]'
})
export class PercentColorChangerDirective {

  constructor(private element: ElementRef, private render: Renderer2) { }

  @Input("d2-percentColorChanger")
  percentValue: string;

  getColorFromPercent(value: string): string {
    if (+value < 45) {
      return "red"
    }
    if (+value > 55) {
      return "green"
    }
    else {
      return "orange"
    }
  }

  ngOnInit() {
    this.render.setStyle(this.element.nativeElement, 'color', this.getColorFromPercent(this.percentValue));
  }

}
