import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http'
import { Observable } from 'rxjs';
import { HelpCall } from '../models/helpCall.model';
import { environment } from 'src/environments/environment';
import { DoctorDetails } from '../models/doctorDelails.model';


@Injectable({
  providedIn: 'root'
})
export class HelpCallService {
  private currentHelpCall: number;
private doctorInThisHelpCall:DoctorDetails;
  constructor(private http:HttpClient) { }

  get CurrnetHelpCall()
  {     
    return this.currentHelpCall;   
  } 
 
  setCurrentHelpCall(helpCallId)
  {     
    this.currentHelpCall = helpCallId;   
  }
  setCurrentDoctorToHelpCall(doctor)
  {     
    this.doctorInThisHelpCall = doctor;   
  }
  getCurrentDoctorToHelpCall()
  {     
   return this.doctorInThisHelpCall;   
  }
  AddHelpCall(helpcall:HelpCall): Observable<boolean> {
    const data = new FormData();
    console.log(helpcall)
    data.append('helpcall', JSON.stringify(helpcall)); 

    return this.http.post<boolean>(environment.url + 'Doctor/AddHelpCall', data)
  }
}
