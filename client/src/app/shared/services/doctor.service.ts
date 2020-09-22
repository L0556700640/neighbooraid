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
  constructor(private http: HttpClient) { }

  addDoctor(doctor: DoctorDetails): Observable<boolean> {
    return this.http.post<boolean>(environment.url + 'Doctor/AddDoctor', doctor)
  }
}