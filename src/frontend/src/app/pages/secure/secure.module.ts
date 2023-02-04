import { NgModule } from '@angular/core';
import { SecureComponent } from './secure.component';
import { CommonModule } from '@angular/common';
import { LayoutModule } from './layout/layout.module';
import { RouterModule } from '@angular/router';
import { SecureRoutingModule } from './secure.routing.module';

@NgModule({
  declarations: [
    SecureComponent
  ],
  imports: [
    SecureRoutingModule,
    CommonModule,
    RouterModule,
    LayoutModule,
  ],
  exports: [
    SecureComponent
  ]
})
export class SecureModule { }
