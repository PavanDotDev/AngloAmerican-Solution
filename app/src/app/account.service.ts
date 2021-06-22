import { Injectable } from '@angular/core';  
import { HttpClient } from '@angular/common/http';  
import { HttpHeaders } from '@angular/common/http';  
import { Observable, BehaviorSubject } from 'rxjs';  
import { AccountModel } from './account/account.model'; 
import { AccountTypeModel } from './account-type/account-type.model'; 
import { AccountGridDataModel } from './account/account-grid-data.model';

@Injectable({  
    providedIn: 'root'  
})

export class AccountService {  
    url = 'https://localhost:44329/';  
    private messageSource = new BehaviorSubject(0);
    private accTypeMsgSrc = new BehaviorSubject([]);
    accTypeSelection = this.messageSource.asObservable();
    accountTypes = this.accTypeMsgSrc.asObservable();

    constructor(private http: HttpClient) { }

    setSelcetedAccTypeValue(accType: number) {
        this.messageSource.next(accType)
    }

    setAccountTypesArr(accTypeArr: any) {
      this.accTypeMsgSrc.next(accTypeArr);
    }

    getAllAccounts() : Observable<AccountGridDataModel[]> {  
      return this.http.get<AccountGridDataModel[]>(this.url+"accounts");  
    }  
    
    getAllAccountTyes() : Observable<AccountTypeModel[]> {  
      return this.http.get<AccountTypeModel[]>(this.url+"accountType");  
    }  
    
    createAccount(account: any) {
      const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };  
        return this.http.post<any>(this.url + 'accounts',  account, httpOptions);  
    }
  }  