import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Cases } from 'src/app/shared/models/cases.model';
import { RelatedDoctorToCases } from 'src/app/shared/models/RelatedDoctorToCases';
import { CasesService } from 'src/app/shared/services/cases.service';
import { DoctorService } from 'src/app/shared/services/doctor.service';
import { RelatedDoctorToCasesService } from 'src/app/shared/services/related-doctor-to-cases.service';
import { VoiceRecognitionService } from 'src/app/shared/services/voice-recognition.service';
import { HelpCallService } from '../../shared/services/help-call.service';

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
  myList=this.allCases;

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
  contactsUrl:String;
  constructor(private casesService: CasesService, public microphoneService: VoiceRecognitionService, private router: Router,private doctorService:DoctorService,private relatedDoctorService:RelatedDoctorToCasesService, private helpCallService:HelpCallService) { 
    this.showFullCasesList();
    this.microphoneService.init();
    this.myList=this.allCases;
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
    this.fullCasesList=true;
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
  searchVoice() 
  {
    let helpCallID=1;
    let sentence=this.microphoneService.text;
    sentence=sentence.split('.').join('');
    this.casesService.GetRelatedCases(helpCallID, sentence).subscribe(res => { this.relatedCases = res; });
    this.fullCasesList = false;
  this.myList=this.relatedCases;
  }
  startMicrophoneService() 
  {
    if(this.isRecording==false)
    {
      this.isRecording = true;
      this.microphoneService.start()
    }
    else
    {
    this.isRecording = false;
    this.microphoneService.stop()
    this.searchVoice();
    console.log('this.sen'+this.microphoneService.text)
    
    }
  }
  getTheProfessionalDoctors(){
    
  }

  clickCases(i) 
  {
    this.googleContacts() ;

  if(this.fullCasesList==true)
  {
    this.casesService.choseCaseAction(this.helpCallService.CurrnetHelpCall,this.allCases[i],this.contactsUrl).subscribe()
    this.casesService.setCurrentCase(this.allCases[i])
    this.doctorService.GetDoctorsToCase(this.helpCallService.CurrnetHelpCall,this.allCases[i].caseId).subscribe(
      res=>{
        this.relatedDoctorService.setCurrentCloseDoctor(res.closeDoctors)
        console.log(res.closeDoctors)
        this.relatedDoctorService.setCurrentcontacts(res.contacts);
      }
    )
  }else
  {
    this.casesService.choseCaseAction(this.helpCallService.CurrnetHelpCall,this.relatedCases[i],this.contactsUrl).subscribe()
    this.casesService.setCurrentCase(this.relatedCases[i])
    this.doctorService.GetDoctorsToCase(this.helpCallService.CurrnetHelpCall,this.relatedCases[i].caseId).subscribe(
      res=>{
        this.relatedDoctorService.setCurrentCloseDoctor(res.closeDoctors)
        this.relatedDoctorService.setCurrentcontacts(res.contacts);
      }
    )
  }
    this.router.navigateByUrl('contacts')
  }
}

