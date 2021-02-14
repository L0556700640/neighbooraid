import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { url } from 'inspector';
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
  url:string;
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

 authConfig: { client_id: string; scope: string; };
  matches: String[];
  isRecording = false;
  constructor(private casesService: CasesService, public microphoneService: VoiceRecognitionService, private router: Router,private doctorService:DoctorService,private relatedDoctorService:RelatedDoctorToCasesService, private helpCallService:HelpCallService) { 
    this.showFullCasesList();
    this.microphoneService.init();
    this.myList=this.allCases;
    console.log(typeof this.myList);
    console.log(this.myList);
  }

  ngOnInit() 
  {
    this.authConfig = {
      client_id: '799010120213-65uv1oe7cl37p0kj4ddnbbd2fcno3sgr.apps.googleusercontent.com',
      scope: 'https://www.googleapis.com/auth/contacts.readonly'
  }
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
  colorCases()
  {
    return this.isChoose[this.i]
  }
  clickCases(i) 
  {
    this.i=i

    this.isChoose[i] = !this.isChoose[i]
    let url
    gapi.client.setApiKey('AIzaSyBrXhPtMorEH1jvdOptRJsshnym-Ut5bw0');
    gapi.auth2.authorize(this.authConfig, (authorizationResult)=> 
    {
      if (authorizationResult && !authorizationResult.error) 
      {
        url = "https://www.google.com/m8/feeds/contacts/default/thin?alt=json&access_token=" + authorizationResult.access_token + "&max-results=500&v=3.0";
        // var url="https://www.google.com/m8/feeds/contacts/default/full?alt=json&access_token=" + authorizationResult.access_token + "&max-results=500&v=3.0";
  
        //   //process the response here
        console.log(url);
       localStorage.setItem("url",url)
       
      }
    })
    
  }
 
  
  goToDoctors()
  {
    this.url=localStorage.getItem("url")
    if(this.fullCasesList==true)
    {
      this.casesService.setCurrentCase(this.allCases[this.i])
      this.casesService.choseCaseAction(this.helpCallService.CurrnetHelpCall,this.allCases[this.i].caseId,this.url).subscribe( 
        (res)=>{
          this.relatedDoctorService.setCurrentCloseDoctor(res.closeDoctor)
          console.log(res.closeDoctor)
          this.relatedDoctorService.setCurrentcontacts(res.contacts);
        })
      
      // this.doctorService.GetDoctorsToCase(this.helpCallService.CurrnetHelpCall,this.allCases[i].caseId).subscribe(
       
      // )
    }else
    {
      this.casesService.choseCaseAction(this.helpCallService.CurrnetHelpCall,this.relatedCases[this.i].caseId,this.url).subscribe( 
        res=>{
          this.relatedDoctorService.setCurrentCloseDoctor(res.closeDoctor)
          this.relatedDoctorService.setCurrentcontacts(res.contacts);
        })
      this.casesService.setCurrentCase(this.relatedCases[this.i])
      // this.doctorService.GetDoctorsToCase(this.helpCallService.CurrnetHelpCall,this.relatedCases[i].caseId).subscribe(
       
      // )
    }
      this.router.navigateByUrl('contacts')
  
  
  }
}

