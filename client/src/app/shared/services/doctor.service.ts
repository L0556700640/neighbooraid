import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Doctor } from '../models/doctor.model';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { DoctorDetails } from '../models/doctorDelails.model';
import { CasesToDoctor } from '../models/caseToDoctor.model';
import { Cases } from '../models/cases.model';
import { RelatedDoctorToCases } from '../models/RelatedDoctorToCases';

@Injectable({
  providedIn: 'root'
})
export class DoctorService {
  data = new FormData();
  doctor: DoctorDetails = new DoctorDetails();
  constructor(private http: HttpClient) { }

  addDoctor(d: Doctor) {
    this.doctor.Doctor = d
  }
  finishAddDoctor(file: File, d: DoctorDetails): Observable<boolean> {
    this.doctor.Doctor.certificateNumber = d.Doctor.certificateNumber
    this.doctor.Doctor.certificateValidity = d.Doctor.certificateValidity
    console.log(this.doctor)
    this.data.append('doctor', JSON.stringify(this.doctor)); 
    
    this.data.append('file', file)

    return this.http.post<boolean>(environment.url + 'Doctor/AddDoctor', this.data)
  }

  AddCasesToDoctor(casestodoctor: Cases[]){
      this.doctor.Cases=casestodoctor
  }

  getCurrentDoctor(id: string): Observable<Doctor> {
    return this.http.get<Doctor>(environment.url + 'Doctor/user/' + id)

  }

  getCurrentCasesToDoctor(id: string): Observable<CasesToDoctor[]> {
    return this.http.get<CasesToDoctor[]>(environment.url + 'Doctor/casesToDoctor/' + id)

  }

  GetDoctorsToCase(helpcallid:number,c: number): Observable<RelatedDoctorToCases> {
    return this.http.get<RelatedDoctorToCases>(environment.url + "Doctor/GetDoctorToCases/"+helpcallid +"/"+ c)

  }

  

  delete(id: string): Observable<boolean> {
    const data = new FormData();
    data.append('id', JSON.stringify(id));
    return this.http.post<boolean>(environment.url + 'Doctor/DeleteDoctor', data)

  }

 idDoctors(id:string): Observable<boolean>
 {
   return this.http.get<boolean>(environment.url + 'Doctor/idDoctor/'+id)
 }
}