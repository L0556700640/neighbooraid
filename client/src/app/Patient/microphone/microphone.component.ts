import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Cases } from 'src/app/shared/models/cases.model';
import { CasesService } from 'src/app/shared/services/cases.service';
import { VoiceRecognitionService } from 'src/app/shared/services/voice-recognition.service';

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
  constructor(private casesService: CasesService, public microphoneService: VoiceRecognitionService, private router: Router) { 
    this.showFullCasesList();
    this.microphoneService.init();
    this.myList=this.allCases;
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
  searchVoice() {
    let helpCallID=1;
    let sentence=this.microphoneService.text;
    sentence=sentence.split('.').join('');
    this.casesService.GetRelatedCases(helpCallID, sentence).subscribe(res => { this.relatedCases = res; });
    this.fullCasesList = false;
  this.myList=this.relatedCases;
  }
  startMicrophoneService() {
    if(this.isRecording==false)
    {
    this.isRecording = true;
    this.microphoneService.start()}
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
    this.casesService.choseCaseAction(1,this.myList[i])
    this.casesService.setCurrentCase(this.myList[i])
    this.router.navigateByUrl('contacts')
  }
}
