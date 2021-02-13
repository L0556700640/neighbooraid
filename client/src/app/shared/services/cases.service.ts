import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http'
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
   
    contactsUrl="https://www.google.com/m8/feeds/contacts/default/thin?alt=json_access_token=ya29.A0AfH6SMAp22-TJXYMU2t2b5kn6r3Tpf9aVPQJcpv0BQDcNpMtyIhFFQplo_MeZ59oDtFkVzY2BfhyXS-0LwyzqDTed8OeVi3vh0s-OOGmDr_3kOB2bZOYU23xdFRxhVRvLjASNm_028EFfkj2FM1A353PNFZHIw_max-results=500_v=3.0"

   
    return this.http.post<RelatedDoctorToCases>(environment.url +"Cases/CaseChose", {helpCallID,choosedCase,contactsUrl});
  }

}