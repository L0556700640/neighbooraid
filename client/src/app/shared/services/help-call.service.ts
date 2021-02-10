import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http'
import { Observable } from 'rxjs';
import { HelpCall } from '../models/helpCall.model';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class HelpCallService {
  private currentHelpCall: number;

  constructor(private http:HttpClient) { }

  get CurrnetHelpCall()
  {     
    return this.currentHelpCall;   
  } 
 
  setCurrentHelpCall(helpCallId)
  {     
    this.currentHelpCall = helpCallId;   
  }

  AddHelpCall(helpcall:HelpCall): Observable<boolean> {
    const data = new FormData();
    console.log(helpcall)
    data.append('helpcall', JSON.stringify(helpcall)); 

    return this.http.post<boolean>(environment.url + 'Doctor/AddHelpCall', data)
  }
}
