import { Component, OnInit } from '@angular/core';
import { Doctor } from 'src/app/shared/models/doctor.model';
// import { DoctorDetails } from 'src/app/shared/models/doctorDelails.model';
import { HelpCallService } from 'src/app/shared/services/help-call.service';

@Component({
  selector: 'app-call',
  templateUrl: './call.component.html',
  styleUrls: ['./call.component.scss'],
})
export class CallComponent implements OnInit {
  doctor: Doctor;
  name;
  timerMin:number;
  timerSec:number;
  constructor(private helpCallService: HelpCallService) {
  this.doctor = helpCallService.getCurrentDoctorToHelpCall;
  this.name=this.doctor.firstName+" "+this.doctor.lastName
  console.log(this.doctor); 

  }

  ngOnInit() { }
 time: number = 0;
  display ;
  interval;

 startTimer() {
    console.log("=====>");
    this.interval = setInterval(() => {
      if (this.time === 0) {
        this.time++;
      } else {
        this.time++;
      }
      this.display=this.transform( this.time)
    }, 1000);
  }
  transform(value: number): string {
       const minutes: number = Math.floor(value / 60);
       return minutes + ':' + (value - minutes * 60);
  }
  pauseTimer() {
    clearInterval(this.interval);
  }

}
