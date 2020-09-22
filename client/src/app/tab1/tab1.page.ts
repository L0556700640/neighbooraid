import { Component } from '@angular/core';
import { Doctor } from '../shared/models/doctor.model';
import { DoctorService } from '../shared/services/doctor.service';
import { DoctorDetails } from '../shared/models/doctorDelails.model';
import { Cases } from '../shared/models/cases.model';
import { CasesService } from '../shared/services/cases.service';

@Component({
  selector: 'app-tab1',
  templateUrl: 'tab1.page.html',
  styleUrls: ['tab1.page.scss']
})
export class Tab1Page {
  page1 = true;
  page2 = false;
  page3 = false;
  finish = false;
 allCases:Cases[]=[];
 doctor: DoctorDetails= new DoctorDetails();
  constructor(private doctorService: DoctorService,private casesService:CasesService ) { 
   this.casesService.getAllCases().subscribe(res=>{this.allCases=res;});
  }

  next1() {
    this.page1 = false;
    this.page2 = true;
    this.doctor.Doctor.isConfirmed = false;
  
  }

  next2() {
    this.page2 = false;
    this.page3 = true;
  }

  next3() {
    
    this.doctorService.addDoctor(this.doctor).subscribe(
      (res)=>{alert(res)}
    )
    this.page3 = false;
    this.finish = true;
  }

  previous1() {
    this.page1 = true;
    this.page2 = false;
  }

  previous2() {
    this.page2 = true;
    this.page3 = false;
  }
}

