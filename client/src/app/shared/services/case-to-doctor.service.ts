import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http'
import { Cases } from '../models/cases.model';
import { Doctor } from '../models/doctor.model';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';



@Injectable({
  providedIn: 'root'
})
export class CaseToDoctorService {
 
  doctorToCases:Doctor[];
    constructor(private http: HttpClient) {
    }

  getDoctorsToCase(selectedCase:Cases) : Observable<Doctor[]> {
 return this.http.get<Doctor[]>(environment.url + 'Doctor/getDoctorToCases/'+selectedCase);
    }
}
