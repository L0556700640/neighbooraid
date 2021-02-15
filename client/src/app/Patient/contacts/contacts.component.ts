import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CasesService } from 'src/app/shared/services/cases.service';
import { DoctorService } from 'src/app/shared/services/doctor.service';
import { HelpCallService } from 'src/app/shared/services/help-call.service';
import { RelatedDoctorToCasesService } from 'src/app/shared/services/related-doctor-to-cases.service';

@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.component.html',
  styleUrls: ['./contacts.component.scss'],
})
export class ContactsComponent implements OnInit {
 case;
 doctors;
  nameDoctor: string[] = [];

 
  constructor(private casesService:CasesService,private doctorService:DoctorService,private relatedDoctorService: RelatedDoctorToCasesService,private router: Router,private helpCallService:HelpCallService) 
  {   
    this.case=this.casesService.CurrnetCase
   
    var j = 0 
    const sleep = (milliseconds) => {
      return new Promise(resolve => setTimeout(resolve, milliseconds))
    }
    sleep(5000).then(() => {
      this.doctors = this.relatedDoctorService.Currnetcontacts
    this.doctors.forEach(d => {
      //this.nameDoctor[j] = (d.Doctor.firstName + " " + d.Doctor.lastName).toString();
      let s = d.Satisfaction.toString() + "%";
      document.close();
     // document.getElementById(d.Doctor.doctorId).style.width = s;
      j++
    });
    })
   
  }
 



  ngOnInit() {
   
   // this.doctor=this.doctorService.GetDoctorsToCase(this.case);
  
  
  }
  calloctor(d) {
    this.helpCallService.setCurrentDoctorToHelpCall(d);
    this.helpCallService.AddDoctorToHelpCall(this.helpCallService.CurrnetHelpCall,d.doctorId).subscribe(
      (res)=>
      {
        console.log(res)
      }
    )
    this.router.navigate(['/call']);
   }
  
}
