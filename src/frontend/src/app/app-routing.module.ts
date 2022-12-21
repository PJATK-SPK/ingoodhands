import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { SecureComponent } from './pages/secure/secure.component';

const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./pages/home/home.module').then(m => m.HomeModule),
    component: HomeComponent
  },
  {
    path: 'secure',
    loadChildren: () => import('./pages/secure/secure.module').then(m => m.SecureModule),
    component: SecureComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
