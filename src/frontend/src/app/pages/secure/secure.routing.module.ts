import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StartComponent } from './start/start.component';
import { ConfirmDonationComponent } from './confirm-donation/confirm-donation.component';
import { MyDonationsComponent } from './my-donations/my-donations.component';
import { UserSettingsComponent } from './user-settings/user-settings.component';
import { MyDonationComponent } from './my-donation/my-donation.component';
import { ManageUsersComponent } from './manage-users/manage-users.component';
import { ManageUserComponent } from './manage-user/manage-user.component';
import { PickupDonationComponent } from './pickup-donation/pickup-donation.component';
import { StocksComponent } from './stocks/stocks.component';
import { WorkComponent } from './work/work.component';
import { RequestHelpComponent } from './request-help/request-help.component';
import { CreateOrderComponent } from './create-order/create-order.component';
import { OrderComponent } from './order/order.component';
import { CreateAddressComponent } from './create-address/create-address.component';

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
    {
        path: 'manage-users',
        loadChildren: () => import('./manage-users/manage-users.module').then(m => m.ManageUsersModule),
        component: ManageUsersComponent
    },
    {
        path: 'manage-user/:id',
        loadChildren: () => import('./manage-user/manage-user.module').then(m => m.ManageUserModule),
        component: ManageUserComponent
    },
    {
        path: 'pickup-donation',
        loadChildren: () => import('./pickup-donation/pickup-donation.module').then(m => m.PickUpDonationModule),
        component: PickupDonationComponent
    },
    {
        path: 'stocks',
        loadChildren: () => import('./stocks/stocks.module').then(m => m.StocksModule),
        component: StocksComponent
    },
    {
        path: 'work',
        loadChildren: () => import('./work/work.module').then(m => m.WorkModule),
        component: WorkComponent
    },
    {
        path: 'request-help',
        loadChildren: () => import('./request-help/request-help.module').then(m => m.RequestHelpModule),
        component: RequestHelpComponent
    },
    {
        path: 'create-order',
        loadChildren: () => import('./create-order/create-order.module').then(m => m.CreateOrderModule),
        component: CreateOrderComponent
    },
    {
        path: 'create-address',
        loadChildren: () => import('./create-address/create-address.module').then(m => m.CreateAddressModule),
        component: CreateAddressComponent
    },
    {
        path: 'order/:id',
        loadChildren: () => import('./order/order.module').then(m => m.OrderModule),
        component: OrderComponent
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class SecureRoutingModule { }
