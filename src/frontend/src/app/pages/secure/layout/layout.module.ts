import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutComponent } from './layout.component';
import { SidebarModule } from 'primeng/sidebar';
import { RippleModule } from 'primeng/ripple';
import { StyleClassModule } from 'primeng/styleclass';
import { InputTextModule } from 'primeng/inputtext';
import { SidebarComponent } from './sidebar/sidebar.component';
import { NavbarComponent } from './navbar/navbar.component';
import { RouterModule } from '@angular/router';
import { TooltipModule } from 'primeng/tooltip';
import { DialogModule } from 'primeng/dialog';
import { ButtonModule } from 'primeng/button';

@NgModule({
  declarations: [
    LayoutComponent,
    SidebarComponent,
    NavbarComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    SidebarModule,
    RippleModule,
    StyleClassModule,
    InputTextModule,
    TooltipModule,
    DialogModule,
    ButtonModule,
  ],
  exports: [
    LayoutComponent
  ]
})
export class LayoutModule { }
