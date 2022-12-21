import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthCheckComponent } from './auth-check/auth-check.component';

const routes: Routes = [
    {
        path: '',
        loadChildren: () => import('./auth-check/auth-check.module').then(m => m.AuthCheckModule),
        component: AuthCheckComponent
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class SecureRoutingModule { }
