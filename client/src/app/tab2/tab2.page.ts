import { TypeofExpr } from '@angular/compiler';
import { ChangeDetectorRef, Component } from '@angular/core';
import { NavController, Platform } from '@ionic/angular';
import { from } from 'rxjs';
import { Cases } from '../shared/models/cases.model';
import { CasesService } from '../shared/services/cases.service';
import { SpeechRecognition } from '@ionic-native/speech-recognition/ngx';
import { VoiceRecognitionService } from '../shared/services/voice-recognition.service';
declare global {
  interface Window {
    webkitSpeechRecognition: any,
  }
}
@Component({
  selector: 'app-tab2',
  templateUrl: 'tab2.page.html',
  styleUrls: ['tab2.page.scss']
})

export class Tab2Page {
  searchPage = true;

  familyPage = false;
  doctorsPage = false;
  responsePage = false;
  allCases: Cases[] = [];
  fullCasesList = true;

  matches: String[];
  isRecording = false;
  constructor(private casesService: CasesService, public microphoneService: VoiceRecognitionService) {
    this.showFullCasesList();
    this.microphoneService.init()
  }
  next1() {
    this.doctorsPage = true;
    this.searchPage = false;

  }
  showFullCasesList() {
    this.casesService.getAllCases().subscribe(res => { this.allCases = res; });
    this.fullCasesList=true;
    }
  searchVoice() {
    let sentence=this.microphoneService.text;
    sentence=sentence.split('.').join('');
   // this.casesService.GetRelatedCases(sentence).subscribe(res => { this.allCases = res; });
    this.fullCasesList = false;
  }
  startMicrophoneService() {
    this.isRecording = true;
    this.microphoneService.start()
  }

  stopMicrophoneService() {
    this.isRecording = false;
    this.microphoneService.stop()
    this.searchVoice();
    console.log('this.sen'+this.microphoneService.text)
  }

}
