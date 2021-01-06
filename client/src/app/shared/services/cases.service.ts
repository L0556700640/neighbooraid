import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Cases } from '../models/cases.model';


@Injectable({
  providedIn: 'root'
})
export class CasesService {
  private currentCase=undefined; 
  constructor(private http: HttpClient) {
  }

  get CurrnetCase()
  {     
    return this.currentCase;   
  } 
 
  setCurrentCase(caseId)
  {     
    this.currentCase = caseId;   
  }

  getAllCases(): Observable<Cases[]> {
    return this.http.get<Cases[]>(environment.url + "Cases/GetAllCases");
  }
  GetRelatedCases(searchSentence) : Observable<Cases[]> {
    return this.http.get<Cases[]>(environment.url + "Cases/GetRelatedCases/" + searchSentence);
  }

  getCurrentCase(id: String): Observable<String> {
    return this.http.get<String>(environment.url + "Cases/getCaseName/" + id);

  }
}
