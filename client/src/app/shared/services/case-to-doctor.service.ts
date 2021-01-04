import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http'
import { Cases } from '../models/cases.model';
import { Doctor } from '../models/doctor.model';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { DoctorDetails } from '../models/doctorDelails.model';

@Injectable({
  providedIn: 'root'
})
export class CaseToDoctorService {
 
  doctorToCases:Doctor[];
    constructor(private http: HttpClient) {
    }

  getDoctorsToCase(selectedCase:Cases) : Observable<Doctor[]> {
 return this.http.get<Doctor[]>(environment.url + 'CaseToDoctor/getDoctorToCases/'+selectedCase);
    }

    // AddCasesToDoctor(casestodoctor: DoctorDetails): Observable<boolean> {
      
    //   const data = new FormData();
    //   data.append('cases', JSON.stringify(casestodoctor.Cases));
      
  
    //   return this.http.post<boolean>(environment.url + 'Doctor/AddDoctor', data)
    // }
}
