import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Cases } from '../models/cases.model';
import { RelatedDoctors } from '../models/relatedDoctors.model';


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
  choseCaseAction(helpCallID,choosedCase):Observable<RelatedDoctors[]>{
  return this.http.get<RelatedDoctors[]>(environment.url +" Cases/CaseChose/"+helpCallID+"/"+choosedCase);
  }
}
