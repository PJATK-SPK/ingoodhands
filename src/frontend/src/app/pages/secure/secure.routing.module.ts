import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StartComponent } from './start/start.component';
import { ConfirmDonationComponent } from './confirm-donation/confirm-donation.component';

const routes: Routes = [
    {
        path: '',
        loadChildren: () => import('./start/start.module').then(m => m.StartModule),
        component: StartComponent
    },
    {
        path: 'confirm-donation',
        loadChildren: () => import('./confirm-donation/confirm-donation.module').then(m => m.ConfirmDonationModule),
        component: ConfirmDonationComponent
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class SecureRoutingModule { }
