import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StartComponent } from './start/start.component';
import { ConfirmDonationComponent } from './confirm-donation/confirm-donation.component';
import { MyDonationsComponent } from './my-donations/my-donations.component';
import { UserSettingsComponent } from './user-settings/user-settings.component';
import { MyDonationComponent } from './my-donation/my-donation.component';

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
    {
        path: 'my-donations',
        loadChildren: () => import('./my-donations/my-donations.module').then(m => m.MyDonationsModule),
        component: MyDonationsComponent
    },
    {
        path: 'my-donation/:id',
        loadChildren: () => import('./my-donation/my-donation.module').then(m => m.MyDonationModule),
        component: MyDonationComponent
    },
    {
        path: 'user-settings',
        loadChildren: () => import('./user-settings/user-settings.module').then(m => m.UserSettingsModule),
        component: UserSettingsComponent
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class SecureRoutingModule { }
