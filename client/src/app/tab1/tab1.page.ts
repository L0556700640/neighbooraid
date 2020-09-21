import { Component } from '@angular/core';
import { Doctor } from '../shared/models/doctor.model';
import { DoctorService } from '../shared/services/doctor.service';

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
  public form = [
    { val: 'כויות', isChecked: false },
    { val: 'חתכים', isChecked: false },
    { val: 'מכות יבשות', isChecked: false },
    { val: 'התחשמלות', isChecked: false },
    { val: 'עקיצה', isChecked: false },
    { val: 'פריחה', isChecked: false },
    { val: 'הקשה', isChecked: false },
    { val: 'נשיכה', isChecked: false },
    { val: 'התקף חרדה', isChecked: false },
    { val: 'רעלים', isChecked: false },
    { val: 'חום', isChecked: false }
  ];
  constructor(private doctorService: DoctorService,  private doctor: Doctor ) { }

  next1() {
    this.page1 = false;
    this.page2 = true;
    this.doctor.firstName = document.getElementById('firstName').innerHTML;
    this.doctor.lastName = document.getElementById('lastName').innerHTML;
    //this.doctor.doctorId=document.getElementById('identityNum').innerHTML;
    this.doctor.address = document.getElementById('address').innerHTML;
    this.doctor.doctorPhone = document.getElementById('phoneNum').innerHTML;
    this.doctor.isConfirmed = false;
  }

  next2() {
    this.page2 = false;
    this.page3 = true;
  }

  next3() {
    this.doctorService.addDoctor(this.doctor);
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

