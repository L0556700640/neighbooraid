import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Cases } from '../models/cases.model';
 import { RelatedDoctorToCases } from '../models/RelatedDoctorToCases'


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
  
  GetRelatedCases(helpCallID, searchSentence) : Observable<Cases[]> {
    return this.http.get<Cases[]>(environment.url + "Cases/GetRelatedCases/" +helpCallID+"/"+ searchSentence);
  }

  getCurrentCase(id: String): Observable<String> {
    return this.http.get<String>(environment.url + "Cases/getCaseName/" + id);

  }
   choseCaseAction(helpCallID,choosedCase,contactsUrl):Observable<RelatedDoctorToCases>{
   return this.http.get<RelatedDoctorToCases>(environment.url +"Cases/CaseChose/"+helpCallID+"/"+choosedCase+"/"+contactsUrl);
   }
}
