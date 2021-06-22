import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountComponent } from './account/account.component';
import { NewAccountComponent } from './new-account/new-account.component';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'accounts'},
  { path: 'accounts',  component: AccountComponent },
  { path: 'new-account',  component: NewAccountComponent }
  ];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
