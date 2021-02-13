import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup } from '@angular/forms';
import { Cases } from 'src/app/shared/models/cases.model';
import { CasesService } from 'src/app/shared/services/cases.service';
import { HelpCallService } from 'src/app/shared/services/help-call.service';
import { DoctorService } from 'src/app/shared/services/doctor.service';
import { RelatedDoctorToCasesService } from 'src/app/shared/services/related-doctor-to-cases.service';

@Component({
  selector: 'app-more-cases',
  templateUrl: './more-cases.component.html',
  styleUrls: ['./more-cases.component.scss'],
})
export class MoreCasesComponent implements OnInit {

  allCases: Cases[] = [];
  myForm: FormGroup;
  isChoose: boolean[] = [];
  len = 0;
  i = 0;
  splitedCases: [Cases[]] = [[]]
  casesToDoctor: Cases[] = []

  clientId = '799010120213-65uv1oe7cl37p0kj4ddnbbd2fcno3sgr.apps.googleusercontent.com';
  apiKey = 'AIzaSyBrXhPtMorEH1jvdOptRJsshnym-Ut5bw0';
  scopes = 'https://www.googleapis.com/auth/contacts.readonly';
  authConfig: { client_id: string; scope: string; };
  contactsUrl:String;

  constructor(private casesService: CasesService, private router: Router, private helpCallService:HelpCallService,private doctorService:DoctorService,private relatedDoctorService:RelatedDoctorToCasesService) {
    this.casesService.getAllCases().subscribe(res => {
      this.allCases = res;
     
      let i = 0;
      for (; i < res.length - 2; i += 3) {
        this.splitedCases.push([res[i], res[i + 1], res[i + 2]]);
        this.isChoose.push(false)
      }
      if (i < this.allCases.length) {
        this.splitedCases.push([]);
        for (; i < res.length; i++) {
          this.splitedCases[this.splitedCases.length - 1].push(this.allCases[i]);
        }
        console.log(this.splitedCases)


      }
      // for (let i = 0; i < this.allCases.length; i++) {
      //   this.isChoose.push(false);
      // }
    });  
  }

  ngOnInit() {

    this.authConfig = {
      client_id: '799010120213-65uv1oe7cl37p0kj4ddnbbd2fcno3sgr.apps.googleusercontent.com',
      scope: 'https://www.googleapis.com/auth/contacts.readonly'
    };
    
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
        this.contactsUrl="https://www.google.com/m8/feeds/contacts/default/thin?alt=json&access_token=" + authorizationResult.access_token + "&max-results=500&v=3.0";
       // var url="https://www.google.com/m8/feeds/contacts/default/full?alt=json&access_token=" + authorizationResult.access_token + "&max-results=500&v=3.0";
 
        //   //process the response here
           console.log(this.contactsUrl);
       
     }
    }
  clickCases(i,j) 
  {

    i--;

    this.casesService.choseCaseAction(this.helpCallService.CurrnetHelpCall,this.allCases[i * 3 + j].caseId,this.contactsUrl).subscribe()
    
    this.casesService.setCurrentCase(this.allCases[i * 3 + j])
    this.doctorService.GetDoctorsToCase(this.helpCallService.CurrnetHelpCall,this.allCases[i* 3 + j].caseId).subscribe(
      res=>{
        this.relatedDoctorService.setCurrentCloseDoctor(res.closeDoctors)
        console.log(res.closeDoctors)
        this.relatedDoctorService.setCurrentcontacts(res.contacts);
      }
    )
    
    this.router.navigateByUrl('contacts')
  }
    
  clickCorrectCase() {
    //to the server
        
  }

}


