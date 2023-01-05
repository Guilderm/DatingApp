import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = 'http://192.168.2.171:5000/api/'

  constructor(private http: HttpClient) { }
  login(model: any) {
    return this.http.post(this.baseUrl + 'account/login', model    )
  }  }
