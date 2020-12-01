import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Doctor } from '../models/doctor.model';

@Injectable({
  providedIn: 'root'
})
export class LoginService 
{
  private currentUser = undefined; 
  constructor(private http: HttpClient) { }

  get IsLogin() 
  {    
    return this.currentUser !== undefined;   
  } 
 
  get CurrnetUser()
  {     
    return this.currentUser;   
  } 
 
  setCurrentUser(user)
  {     
    this.currentUser = user;   
  }

  chackUser(firstName:string,id:string): Observable<boolean> 
  {
    const data=new FormData();
    data.append('firstName',JSON.stringify(firstName));
    data.append('id',JSON.stringify(id));
    return this.http.post<boolean>(environment.url + 'Doctor/CheckUser', data)
  }
} 
