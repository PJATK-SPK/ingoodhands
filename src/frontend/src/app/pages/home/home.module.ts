import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { StyleClassModule } from 'primeng/styleclass';
import { TabViewModule } from 'primeng/tabview';
import { NavbarComponent } from './navbar/navbar.component';
import { DinnerComponent } from './dinner/dinner.component';

@NgModule({
  declarations: [
    HomeComponent,
    NavbarComponent,
    DinnerComponent,
  ],
  imports: [
    CommonModule,
    StyleClassModule,
    TabViewModule,
  ],
  exports: [
    HomeComponent
  ]
})
export class HomeModule { }
