import { Component, OnInit } from '@angular/core';
import { CasesService } from 'src/app/shared/services/cases.service';
import { DoctorService } from 'src/app/shared/services/doctor.service';

@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.component.html',
  styleUrls: ['./contacts.component.scss'],
})
export class ContactsComponent implements OnInit {
 case;
 doctor;
  constructor(private casesService:CasesService,private doctorService:DoctorService) { }

  ngOnInit() {
    this.case=this.casesService.CurrnetCase
    this.doctor=this.doctorService.GetDoctorsToCase(this.case);
  }

}
