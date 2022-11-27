import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LandingComponent } from './pages/landing/landing.component';
import { MainComponent } from './pages/application/main/main.component';

const routes: Routes = [
  // {
  //   path: '',
  //   loadChildren: () => import('./pages/landing/landing.module').then(m => m.LandingModule),
  //   component: LandingComponent
  // },
  {
    path: '',
    loadChildren: () => import('./pages/application/main/main.module').then(m => m.MainModule),
    component: MainComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
