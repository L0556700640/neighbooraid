import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http'
import { Observable } from 'rxjs';
import { HelpCall } from '../models/helpCall.model';
import { environment } from 'src/environments/environment';
// import { DoctorDetails } from '../models/doctorDelails.model';
import { Doctor } from '../models/doctor.model';


@Injectable({
  providedIn: 'root'
})
export class HelpCallService {
  private currentHelpCall: number;
private doctorInThisHelpCall:Doctor;
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
  get getCurrentDoctorToHelpCall()
  {     
   return this.doctorInThisHelpCall;   
  }
  AddHelpCall(helpcall:HelpCall): Observable<boolean> {
    const data = new FormData();
    console.log(helpcall)
    data.append('helpcall', JSON.stringify(helpcall)); 

    return this.http.post<boolean>(environment.url + 'Doctor/AddHelpCall', data)
  }

   AddDoctorToHelpCall(helpCallid:number,doctorid:string):Observable<boolean>
   {
    const data = new FormData();
    data.append('helpCallid', JSON.stringify(helpCallid)); 
    data.append('doctorid', JSON.stringify(doctorid)); 
   return this.http.post<boolean>(environment.url + "Doctor/UpdateDoctorToHelpCall",data)
   }
}
