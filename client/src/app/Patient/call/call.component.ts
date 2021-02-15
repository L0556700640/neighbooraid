import { Component, OnInit } from '@angular/core';
// import { DoctorDetails } from 'src/app/shared/models/doctorDelails.model';
import { HelpCallService } from 'src/app/shared/services/help-call.service';
import { Doctor } from '../shared/models/doctor.model';

@Component({
  selector: 'app-call',
  templateUrl: './call.component.html',
  styleUrls: ['./call.component.scss'],
})
export class CallComponent implements OnInit {
  doctor: Doctor;
  name
  constructor(private helpCallService: HelpCallService) {
  this.doctor = helpCallService.getCurrentDoctorToHelpCall;
  this.name=this.doctor.firstName+" "+this.doctor.lastName
  console.log(this.doctor)
  }

  ngOnInit() { }

}
