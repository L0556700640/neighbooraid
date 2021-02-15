import { Component, OnInit } from '@angular/core';
import { DoctorDetails } from 'src/app/shared/models/doctorDelails.model';
import { HelpCallService } from 'src/app/shared/services/help-call.service';

@Component({
  selector: 'app-call',
  templateUrl: './call.component.html',
  styleUrls: ['./call.component.scss'],
})
export class CallComponent implements OnInit {
  doctor: DoctorDetails
  constructor(private helpCallService: HelpCallService) {
  this.doctor = helpCallService.getCurrentDoctorToHelpCall();
  console.log(this.doctor)
  }

  ngOnInit() { }

}
