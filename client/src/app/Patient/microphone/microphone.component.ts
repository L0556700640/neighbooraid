import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { kStringMaxLength } from 'buffer';
import { Cases } from 'src/app/shared/models/cases.model';
import { RelatedDoctorToCases } from 'src/app/shared/models/RelatedDoctorToCases';
import { CasesService } from 'src/app/shared/services/cases.service';
import { DoctorService } from 'src/app/shared/services/doctor.service';
import { RelatedDoctorToCasesService } from 'src/app/shared/services/related-doctor-to-cases.service';
import { VoiceRecognitionService } from 'src/app/shared/services/voice-recognition.service';
import { HelpCallService } from '../../shared/services/help-call.service';
import { readFile, write, writeFile } from 'fs'
@Component({
  selector: 'app-microphone',
  templateUrl: './microphone.component.html',
  styleUrls: ['./microphone.component.scss'],
})
export class MicrophoneComponent implements OnInit {
  doctorsPage = false;
  responsePage = false;

  allCases: Cases[] = [];
  relatedCases: Cases[] = [];
  myList = this.allCases;

  fullCasesList = true;
  isChoose: boolean[] = [];
  len = 0;
  i = 0;
  splitedCases: [Cases[]] = [[]]
  casesToDoctor: Cases[] = []


  matches: String[];
  isRecording = false;

  clientId = '799010120213-65uv1oe7cl37p0kj4ddnbbd2fcno3sgr.apps.googleusercontent.com';
  apiKey = 'AIzaSyBrXhPtMorEH1jvdOptRJsshnym-Ut5bw0';
  scopes = 'https://www.googleapis.com/auth/contacts.readonly';
  authConfig: { client_id: string; scope: string; };
  contactsUrl: string;
  constructor(private casesService: CasesService, public microphoneService: VoiceRecognitionService, private router: Router, private doctorService: DoctorService, private relatedDoctorService: RelatedDoctorToCasesService, private helpCallService: HelpCallService) {
    this.showFullCasesList();
    this.microphoneService.init();
    this.myList = this.allCases;
    console.log(typeof this.myList);
    console.log(this.myList);



  }


  ngOnInit() {
    this.authConfig = {
      client_id: '799010120213-65uv1oe7cl37p0kj4ddnbbd2fcno3sgr.apps.googleusercontent.com',
      scope: 'https://www.googleapis.com/auth/contacts.readonly'
    };
  }
  showFullCasesList() {
    this.casesService.getAllCases().subscribe(
      res => {
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
      });
    this.fullCasesList = true;
  }
  async googleContacts() {

    gapi.client.setApiKey('AIzaSyBrXhPtMorEH1jvdOptRJsshnym-Ut5bw0');

  await window.gapi.auth2.authorize(this.authConfig,await this.handleAuthorization);
    // gapi.client.setApiKey(this.apiKey);
   //  window.setTimeout(this.authorize);

  }

 async authorize() {
   
    
    await gapi.auth.authorize({ client_id: this.clientId, scope: this.scopes, immediate: false }, this.handleAuthorization);

  }

 async handleAuthorization(authorizationResult) {
    let s:string;
    if (authorizationResult && !authorizationResult.error) {
      s = "https://www.google.com/m8/feeds/contacts/default/thin?alt=json&access_token=" + authorizationResult.access_token + "&max-results=500&v=3.0";
      console.log(s);
     // return s;
     localStorage.setItem("url",s);  
    }

  }
  searchVoice() {
    let helpCallID = 1;
    let sentence = this.microphoneService.text;
    sentence = sentence.split('.').join('');
    this.casesService.GetRelatedCases(helpCallID, sentence).subscribe(res => { this.relatedCases = res; });
    this.fullCasesList = false;
    this.myList = this.relatedCases;
  }
  startMicrophoneService() {
    if (this.isRecording == false) {
      this.isRecording = true;
      this.microphoneService.start()
    }
    else {
      this.isRecording = false;
      this.microphoneService.stop()
      this.searchVoice();
      console.log('this.sen' + this.microphoneService.text)

    }
  }
  getTheProfessionalDoctors() {

  }

  clickCases(i) {
    this.i=i;
    console.log(this.i);
    this.googleContacts().then(res=>
      {
        this.contactsUrl=localStorage.getItem("url");
        console.log(this.contactsUrl+"this true");
      
    //this.contactsUrl="ya29.a0AfH6SMDUBUYpexaB7Cup1hW-ZVi1iP3jUkrgHk33zNWVue4To6K02oz-G3QMVsjbGcovaUZKIxQV_i7RxvBucCMGzYTVML3vC8sOzTfzewJDL4Ibc7ko-v7YYZNnRPeehbVnw_jnh_yyBC00AZJIPj5MEkfowpDt7NtpPt-v8Qx2&max-results=500&v=3.0"
  
  //sendCaseChosedToRheServer(i) {
    if (this.fullCasesList == true) {
      this.casesService.choseCaseAction(this.helpCallService.CurrnetHelpCall, this.allCases[i].caseId, this.contactsUrl).subscribe()
      this.casesService.setCurrentCase(this.allCases[i])
      this.doctorService.GetDoctorsToCase(this.helpCallService.CurrnetHelpCall, this.allCases[i].caseId).subscribe(
        res => {
          this.relatedDoctorService.setCurrentCloseDoctor(res.closeDoctors)
          console.log(res.closeDoctors)
          this.relatedDoctorService.setCurrentcontacts(res.contacts);
        }
      )
    } else {
      this.casesService.choseCaseAction(this.helpCallService.CurrnetHelpCall, this.relatedCases[i].caseId, this.contactsUrl).subscribe()
      this.casesService.setCurrentCase(this.relatedCases[i])
      this.doctorService.GetDoctorsToCase(this.helpCallService.CurrnetHelpCall, this.relatedCases[i].caseId).subscribe(
        res => {
          this.relatedDoctorService.setCurrentCloseDoctor(res.closeDoctors)
          this.relatedDoctorService.setCurrentcontacts(res.contacts);
        }
      )
    }
    this.router.navigateByUrl('contacts')
  });
  }
}


