import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { Address } from 'ngx-google-places-autocomplete/objects/address';
import { Cases } from 'src/app/shared/models/cases.model';
import { Doctor } from 'src/app/shared/models/doctor.model';
import { DoctorDetails } from 'src/app/shared/models/doctorDelails.model';
import { CasesService } from 'src/app/shared/services/cases.service';
import { DoctorService } from 'src/app/shared/services/doctor.service';

@Component({
  selector: 'app-personal-information',
  templateUrl: './personal-information.component.html',
  styleUrls: ['./personal-information.component.scss'],
})
export class PersonalInformationComponent implements OnInit {
  imagePath;
  imageProvider;
  allCases: Cases[] = [];
  myForm: FormGroup;
  doctor: DoctorDetails = new DoctorDetails();
  constructor(private doctorService: DoctorService, private casesService: CasesService,private router: Router) {
    this.casesService.getAllCases().subscribe(res => { this.allCases = res; });
  }

  ngOnInit() {
    this.myForm = new FormGroup({
      firstName: new FormControl(),
      lastName:new FormControl(),
      phone:new FormControl(),
      mail:new FormControl(),
      id:new FormControl(),
      address:new FormControl()
   });
   }

 
  handleDestinationChange(a: Address) {
    this.doctor.Doctor.address = (a.formatted_address);
    console.log(a)
  }

  next()
  {

//     this.doctor.Doctor.firstName=this.myForm.controls.firstName.value;
//     this.doctor.Doctor.lastName=this.myForm.controls.lastName.value;
//     this.doctor.Doctor.doctorId=this.myForm.controls.id.value;
//     this.doctor.Doctor.doctorPhone=this.myForm.controls.phone.value;
//     this.doctor.Doctor.address=this.myForm.controls.address.value;
    this.doctorService.addDoctor(this.doctor)
    this.router.navigateByUrl('cases')
  }
}
