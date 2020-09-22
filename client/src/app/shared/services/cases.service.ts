import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Cases } from '../models/cases.model';


@Injectable({
  providedIn: 'root'
})
export class CasesService {

  constructor(private http: HttpClient) {
  }

  getAllCases(): Observable<Cases[]> {
    return this.http.get<Cases[]>(environment.url + "Cases/GetAllCases");
  }
}
