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
  constructor(private casesService: CasesService, public microphoneService: VoiceRecognitionService, private router: Router,private doctorService:DoctorService,private relatedDoctorService:RelatedDoctorToCasesService, private helpCallService:HelpCallService) { 
    this.showFullCasesList();
    this.microphoneService.init();
    this.myList=this.allCases;
    console.log(typeof this.myList);
    console.log(this.myList);
  }

  ngOnInit() {}
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

  clickCases(i) 
  {
  if(this.fullCasesList==true)
  {
    this.casesService.choseCaseAction(1,this.allCases[i])
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
    this.casesService.choseCaseAction(1,this.relatedCases[i])
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

