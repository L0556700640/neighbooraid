import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { closeDoctor } from '../models/closeDoctor.model';
import { Contact } from '../models/contacts.model';


@Injectable({
  providedIn: 'root'
})
export class RelatedDoctorToCasesService {
  private CloseDoctor: closeDoctor[];
  private contacts: Contact[];

  constructor(private http: HttpClient) { }

  get CurrnetCloseDoctor()
  {     
    return this.CloseDoctor;   
  } 

  get Currnetcontacts()
  {     
    return this.contacts;   
  } 
 
  setCurrentCloseDoctor(close:closeDoctor[])
  {     
    this.CloseDoctor = close;   
  }

  setCurrentcontacts(contact:Contact[])
  {     
    this.contacts = contact;   
  }
}


