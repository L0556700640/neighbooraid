import { Component, OnInit } from '@angular/core';
import { String } from 'lodash';
import { CasesService } from 'src/app/shared/services/cases.service';
import { RelatedDoctorToCasesService } from 'src/app/shared/services/related-doctor-to-cases.service';
@Component({
  selector: 'app-location',
  templateUrl: './location.component.html',
  styleUrls: ['./location.component.scss'],
})
export class LocationComponent implements OnInit {
  case;
  doctors;
  nameDoctor:string[]=[];
 
  constructor(private casesService:CasesService,private relatedDoctorService:RelatedDoctorToCasesService) {   
    this.case=this.casesService.CurrnetCase
    this.doctors=this.relatedDoctorService.CurrnetCloseDoctor
    var j=0
    this.doctors.forEach(d => 
      { 
           this.nameDoctor[j]=(d.Doctor.firstName+" "+d.Doctor.lastName).toString();
          let s = d.Satisfaction.toString() + "%";
         
         // document.getElementById(d.Doctor.doctorId).style.width = s;
        j++
        });}

  ngOnInit() {
  
      
  }
}
