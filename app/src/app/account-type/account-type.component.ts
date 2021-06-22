import { Component, OnInit } from '@angular/core';
import { AccountService } from '../account.service';
import { AccountTypeModel } from './account-type.model';

@Component({
  selector: 'app-account-type',
  templateUrl: './account-type.component.html'
})
export class AccountTypeComponent implements OnInit {

  /* TODO:
  - Load Accounts Types from the REST Api
  - Observable should be used to notify the account.component about changes in the selected Type
   */

  constructor(private accountService: AccountService) {
  }

  public accountTypes: AccountTypeModel[];
  public selectedAccountType : any;

  ngOnInit(): void {
    this.getAccountTypes();
  }

  getAccountTypes(){
    this.accountService.getAllAccountTyes().subscribe(res=>{
      this.accountTypes = res;
      this.accountService.setAccountTypesArr(res);
    });
  }

  onAccountTypeChange(event){
    this.selectedAccountType = event.target.value;
    this.accountService.setSelcetedAccTypeValue(this.selectedAccountType);
  }

}
