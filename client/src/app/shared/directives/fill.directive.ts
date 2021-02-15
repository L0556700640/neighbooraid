import { Directive, ElementRef, Input } from '@angular/core';

@Directive({
  selector: '[appFill]'
})
export class FillDirective {


@Input('appFill') set fill(val){
  this.fillElement(val)
}
  constructor(private el: ElementRef) {
    el.nativeElement.style.width = `${this.fill}%` 
   }
  fillElement(val)
{
  this.el.nativeElement.style.width = `${val}%`
}
}
