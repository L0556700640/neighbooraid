import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CasesService } from 'src/app/shared/services/cases.service';
import { HelpCallService } from 'src/app/shared/services/help-call.service';

@Component({
  selector: 'app-criticism',
  templateUrl: './criticism.component.html',
  styleUrls: ['./criticism.component.scss'],
})
export class CriticismComponent implements OnInit {
case;
doctor;
name;
point:string
  constructor(private router: Router, private casesService: CasesService, private helpCallService:HelpCallService)
  {
    // this.case = this.casesService.CurrnetCase
    // this.doctor = this.helpCallService.getCurrentDoctorToHelpCall
    // this.name=this.doctor.firstName+" "+this.doctor.lastName
    this.point='0'
  }

  ngOnInit() {}

}
