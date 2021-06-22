import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AccountService } from '../account.service';
import { AccountGridDataModel } from './account-grid-data.model';
import {AccountTypeModel} from '../account-type/account-type.model'
@Component({
  selector: 'app-account',
  templateUrl: './account.component.html'
})
export class AccountComponent implements OnInit {

  /* TODO:
  - Load Accounts from the REST Api
  - Display Accounts in the HTML Table
  - Filter Accounts based on the Account Type
   */

  constructor(private accountService : AccountService, private router: Router) {
  }

  public accounts:AccountGridDataModel[];
  accountsList : AccountGridDataModel[];
  selectedAccType: any;
  accTypeList : AccountTypeModel[];

  ngOnInit(): void {
    this.loadAllAccounts();
    this.accountService.accTypeSelection.subscribe(accType => {
      this.selectedAccType = accType
      if(this.accounts!==undefined){
        if(accType!=0){
          let newArr = this.accountsList.filter((acc)=>{return acc.typeId == accType});
          this.accounts = newArr;
        }else{
          this.accounts = this.accountsList;
        }
      }
    });
    this.accountService.accountTypes.subscribe(accTypeArr => {
      this.accTypeList = accTypeArr;      
    });
  }

  loadAllAccounts(){
    this.accountService.getAllAccounts().subscribe(res=>{
      this.accounts = res;
      this.accountsList = res
    });
  }

  getAccName(typeId: any){
    let obj = this.accTypeList.find(e=>{return e.id==typeId});
    return obj.name;
  }

  openNewAccount(): void {
     this.router.navigate(['/new-account']);
  }
}
