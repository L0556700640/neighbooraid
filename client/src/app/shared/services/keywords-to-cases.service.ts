import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Cases } from '../models/cases.model';
import { Doctor } from '../models/doctor.model';

@Injectable({
  providedIn: 'root'
})
export class KeywordsToCasesService {

  constructor( private http:HttpClient) { }
 
 
  saveKeywordsToCase(selectedCase:Cases) : Observable<boolean> {
    return this.http.get<boolean>(environment.url + 'KeywordsToCases/saveKeywordsToCase/'+selectedCase);
       }
}
