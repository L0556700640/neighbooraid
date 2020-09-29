import { ChangeDetectorRef, Component } from '@angular/core';
import { NavController, Platform } from '@ionic/angular';
import { from } from 'rxjs';
import { Cases } from '../shared/models/cases.model';
import { CasesService } from '../shared/services/cases.service';
import { SpeechRecognition } from '@ionic-native/speech-recognition/ngx';
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
  matches: String[];
  isRecording = false;
  allCases: Cases[] = [];
  constructor(private casesService: CasesService) {
    this.casesService.getAllCases().subscribe(res => { this.allCases = res; });
  }
  next1() {
    this.doctorsPage = true;
    this.searchPage = false;

  }
  searchVoice() {
    
  }
  languageOptions=[];
  
}
