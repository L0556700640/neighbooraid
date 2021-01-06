import { Component } from '@angular/core';
import { Doctor } from '../shared/models/doctor.model';
import { DoctorService } from '../shared/services/doctor.service';
import { DoctorDetails } from '../shared/models/doctorDelails.model';
import { Cases } from '../shared/models/cases.model';
import { CasesService } from '../shared/services/cases.service';
import {Address} from 'ngx-google-places-autocomplete/objects/address';
import { FormGroup } from '@angular/forms';


@Component({
  selector: 'app-tab1',
  templateUrl: 'tab1.page.html',
  styleUrls: ['tab1.page.scss']
})
export class Tab1Page {
  page1 = true;
  page2 = false;
  finish = false;
  imagePath;
  imageProvider;
  allCases:Cases[]=[];
  myForm:FormGroup;  
  doctor: DoctorDetails= new DoctorDetails();
  constructor(private doctorService: DoctorService,private casesService:CasesService ) { 
   this.casesService.getAllCases().subscribe(res=>{this.allCases=res;});
  }

   next() {
     if(this.page1==true)
     {
       this.page1 = false;
       this.page2 = true;
       this.doctor.Doctor.isConfirmed = false;
     }
     else
     {
       this.page2 = false;
       this.finish = true; 
       this.doctorService.addDoctor(this.doctor,this.imagePath).subscribe(
         (res)=>{alert(res)}
       )
     }
  

   }

  previous() {
    this.page1 = true;
    this.page2 = false;
  }
  
  changeListener($event): void 
  {
    this.imagePath = $event.target.files[0];
    console.log($event.target.files[0])
    //this.imageProvider.uploadImage(this.imagePath)
  }

  handleDestinationChange(a:Address){
    this.doctor.Doctor.address=(a.formatted_address);
    console.log(a)
  }
}

