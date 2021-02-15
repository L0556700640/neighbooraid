import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { String } from 'lodash';
import { closeDoctor } from 'src/app/shared/models/closeDoctor.model';
import { CasesService } from 'src/app/shared/services/cases.service';
import { HelpCallService } from 'src/app/shared/services/help-call.service';
import { RelatedDoctorToCasesService } from 'src/app/shared/services/related-doctor-to-cases.service';
@Component({
  selector: 'app-location',
  templateUrl: './location.component.html',
  styleUrls: ['./location.component.scss'],
})
export class LocationComponent implements OnInit {
  case;
  doctors: closeDoctor[] = [];

  constructor(private router: Router, private casesService: CasesService, private relatedDoctorService: RelatedDoctorToCasesService, private helpCallService:HelpCallService) {
    console.log(this)


  }

  ngOnInit() {
    window.parent.close();
    this.case = this.casesService.CurrnetCase
    this.doctors = this.relatedDoctorService.CurrnetCloseDoctor
    var j = 0
    this.doctors.forEach(d => {
      // let s = d.Satisfaction.toString() + "%";

     // console.log(document.getElementById(d.Doctor.doctorId))//.style.width = s;
      j++
    });

  }
  callDoctor(d) {
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
