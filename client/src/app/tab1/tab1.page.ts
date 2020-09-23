import { Component } from '@angular/core';
import { Doctor } from '../shared/models/doctor.model';
import { DoctorService } from '../shared/services/doctor.service';
import { DoctorDetails } from '../shared/models/doctorDelails.model';
import { Cases } from '../shared/models/cases.model';
import { CasesService } from '../shared/services/cases.service';
import { promise } from 'protractor';
import {Address} from 'ngx-google-places-autocomplete/objects/address';
import {GooglePlaceDirective} from 'ngx-google-places-autocomplete';


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
  allCases: Cases[] = [];
  chooseCases = []
  doctor: DoctorDetails = new DoctorDetails();
  constructor(private doctorService: DoctorService, private casesService: CasesService) {
    this.casesService.getAllCases().subscribe(res => { this.allCases = res; });
  }

  next() {
    if (this.page1 == true) {
      this.page1 = false;
      this.page2 = true;
      this.doctor.Doctor.isConfirmed = false;
    }
    else {
      this.page2 = false;
      this.finish = true;
      this.doctorService.addDoctor(this.doctor,this.myFile).subscribe(
        (res) => { alert(res) }
      )
    }


  }


 handleDestinationChange(a:Address){
    
    console.log(a)
  }

  previous() {
    this.page1 = true;
    this.page2 = false;
  }
  // next3() {
  //   this.page3 = false;
  //   this.finish = true;
  // }

  // previous2() {
  //   this.page2 = true;
  //   this.page3 = false;
  // }
myFile:File;
  changeListener($event): void {
     this.myFile = $event.target.files[0];

    
    
    ;
  }


//   getByteArray(file:File) {
//     let fileReader = new FileReader();
//     return new Promise(function(resolve, reject) {
//         fileReader.readAsArrayBuffer(file);
//         fileReader.onload = function(ev) {
//             const array = new Uint8Array(ev.target.result as Uint8Array);
//             const fileByteArray = [];
//             for (let i = 0; i < array.length; i++) {
//                 fileByteArray.push(array[i]);
//             }
//             resolve(array);  // successful
//         }
//         fileReader.onerror = reject; // call reject if error
//     })
//  }  
}


