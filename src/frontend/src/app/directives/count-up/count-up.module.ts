import { NgModule } from '@angular/core';
import { CountUpDirective } from './count-up-directive';

@NgModule({
  declarations: [
    CountUpDirective
  ],
  exports: [
    CountUpDirective
  ]
})
export class CountUpModule { }
