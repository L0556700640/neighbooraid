import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { Address } from 'ngx-google-places-autocomplete/objects/address';
import { Cases } from 'src/app/shared/models/cases.model';
import { Doctor } from 'src/app/shared/models/doctor.model';
import { DoctorDetails } from 'src/app/shared/models/doctorDelails.model';
import { CasesService } from 'src/app/shared/services/cases.service';
import { DoctorService } from 'src/app/shared/services/doctor.service';
import { LoginService } from 'src/app/shared/services/login.service';

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
  user: DoctorDetails = new DoctorDetails()
  constructor(private doctorService: DoctorService, private loginService: LoginService, private casesService: CasesService, private router: Router) {
    this.casesService.getAllCases().subscribe(res => { this.allCases = res; });

  }

  ngOnInit() {
    this.myForm = new FormGroup({
      firstName: new FormControl(),
      lastName: new FormControl(),
      phone: new FormControl(),
      mail: new FormControl(),
      id: new FormControl(),
      address: new FormControl()
    });
    this.doctorService.getCurrentDoctor(this.loginService.CurrnetUser).subscribe(
      res => {
        if (this.loginService.IsLogin) {
          this.user.Doctor = res;
          this.myForm.controls.firstName.setValue(this.user.Doctor.firstName)
          this.myForm.controls.lastName.setValue(this.user.Doctor.lastName)
          this.myForm.controls.id.setValue(this.user.Doctor.doctorId)
          this.myForm.controls.phone.setValue(this.user.Doctor.doctorPhone)
          this.myForm.controls.mail.setValue(this.user.Doctor.mail)
          this.myForm.controls.address.setValue(this.user.Doctor.address)
          console.log(this.user.Doctor);
        }

      });




  }


  handleDestinationChange(a: Address) {
    this.doctor.Doctor.address = (a.formatted_address);
    console.log(a)
  }

  next() {
    this.doctorService.addDoctor(this.doctor)
    this.router.navigateByUrl('cases')
  }
}
