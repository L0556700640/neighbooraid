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
  constructor(private casesService:CasesService,private doctorService:DoctorService) { }

  clientId = '799010120213-65uv1oe7cl37p0kj4ddnbbd2fcno3sgr.apps.googleusercontent.com';
  apiKey = 'AIzaSyBrXhPtMorEH1jvdOptRJsshnym-Ut5bw0';
  scopes = 'https://www.googleapis.com/auth/contacts.readonly';

  ngOnInit() {
    this.case=this.casesService.CurrnetCase
   // this.doctor=this.doctorService.GetDoctorsToCase(this.case);
   this.authConfig = {
    client_id: '799010120213-65uv1oe7cl37p0kj4ddnbbd2fcno3sgr.apps.googleusercontent.com',
    scope: 'https://www.googleapis.com/auth/contacts.readonly'
  };
  //this.googleContacts() ;
  }

  googleContacts(){

    gapi.client.setApiKey('AIzaSyBrXhPtMorEH1jvdOptRJsshnym-Ut5bw0');
    gapi.auth2.authorize(this.authConfig, this.handleAuthorization);
           // gapi.client.setApiKey(this.apiKey);
            // window.setTimeout(this.authorize);
    }
 
    authorize() {
     gapi.auth.authorize({client_id: this.clientId, scope: this.scopes, immediate: false}, this.handleAuthorization);
   }
 
   handleAuthorization(authorizationResult) {
     if (authorizationResult && !authorizationResult.error) {
       var url="https://www.google.com/m8/feeds/contacts/default/thin?alt=json&access_token=" + authorizationResult.access_token + "&max-results=500&v=3.0";
       // var url="https://www.google.com/m8/feeds/contacts/default/full?alt=json&access_token=" + authorizationResult.access_token + "&max-results=500&v=3.0";
 
        //   //process the response here
           console.log(url);
       
     }
   }
}
