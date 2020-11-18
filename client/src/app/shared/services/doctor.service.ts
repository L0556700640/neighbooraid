import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Doctor } from '../models/doctor.model';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { DoctorDetails } from '../models/doctorDelails.model';

@Injectable({
  providedIn: 'root'
})
export class DoctorService {
  data=new FormData();

  constructor(private http: HttpClient) { }
addDoctor(doctor: DoctorDetails)
{
 this.data.append('doctor',JSON.stringify(doctor));
}
  finishAddDoctor(file:File): Observable<boolean> 
  {
    this.data.append('file',file)
    return this.http.post<boolean>(environment.url + 'Doctor/AddDoctor', this.data)
  }
}