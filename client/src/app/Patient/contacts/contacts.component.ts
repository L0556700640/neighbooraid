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
 authConfig: { client_id: string; scope: string; };
  constructor(private casesService:CasesService,private doctorService:DoctorService) 
  {  this.case=this.casesService.CurrnetCase}



  ngOnInit() {
   
   // this.doctor=this.doctorService.GetDoctorsToCase(this.case);
   this.authConfig = {
    client_id: '799010120213-65uv1oe7cl37p0kj4ddnbbd2fcno3sgr.apps.googleusercontent.com',
    scope: 'https://www.googleapis.com/auth/contacts.readonly'
  };
  
  }

  
}
