import { Component, OnInit } from '@angular/core';
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
  fullCasesList = true;

  matches: String[];
  isRecording = false;
  constructor(private casesService: CasesService, public microphoneService: VoiceRecognitionService) { 
    this.showFullCasesList();
    this.microphoneService.init() 
  }

  ngOnInit() {}
  showFullCasesList() {
    this.casesService.getAllCases().subscribe(res => { this.allCases = res; });
    this.fullCasesList=true;
    }
  searchVoice() {
    let helpCallID=1;
    let sentence=this.microphoneService.text;
    sentence=sentence.split('.').join('');
    this.casesService.GetRelatedCases(helpCallID, sentence).subscribe(res => { this.relatedCases = res; });
    this.fullCasesList = false;
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
}
