import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
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
  up=false;
  allCases: Cases[] = [];
  myForm: FormGroup;
  doctor: DoctorDetails = new DoctorDetails();
  user: DoctorDetails = new DoctorDetails()
  constructor(private doctorService: DoctorService, private loginService: LoginService, private casesService: CasesService, private router: Router) {
    this.casesService.getAllCases().subscribe(res => { this.allCases = res; });

  }

  ngOnInit() {
    this.myForm = new FormGroup({
      firstName: new FormControl('', Validators.compose([Validators.required, Validators.pattern('[א-ת ]*')])),
      lastName: new FormControl('', Validators.compose([Validators.required, Validators.pattern('[א-ת ]*')])),
      phone: new FormControl('', Validators.compose([Validators.required, Validators.pattern('[0-9]{10}')])),
      mail: new FormControl('', Validators.required),
      id: new FormControl('', Validators.compose([Validators.required, Validators.pattern('[0-9]*')])),
      address: new FormControl('', Validators.required)
    });

    this.doctorService.getCurrentDoctor(this.loginService.CurrnetUser).subscribe(
      res => {
        if (this.loginService.IsLogin) {
          this.user.Doctor = res;
          this.doctorService.update = true
          this.myForm.controls.firstName.setValue(this.user.Doctor.firstName)
          this.myForm.controls.lastName.setValue(this.user.Doctor.lastName)
          this.myForm.controls.id.setValue(this.user.Doctor.doctorId)
          this.myForm.controls.phone.setValue(this.user.Doctor.doctorPhone)
          this.myForm.controls.mail.setValue(this.user.Doctor.mail)
          this.myForm.controls.address.setValue(this.user.Doctor.address)
          this.up=true

          console.log(this.user.Doctor);
        }

      });




  }



  handleDestinationChange(a: Address) {
    this.doctor.Doctor.address = (a.formatted_address);
    console.log(a)
  }

  next() {
    this.doctorService.GetDoctors().subscribe(
      res => {
        if (!this.doctorService.update) {
          res.forEach(d => {
            if (d.doctorId == this.myForm.controls.id.value && d.isConfirmed == true)
              this.myForm.controls.id.setValue("תעודת הזהות כבר קימת במערכת")

          });
        }

        if (this.myForm.valid) {
          if (this.doctorService.update) {
            this.doctorService.updateDoctor = this.doctor
            console.log(this.doctorService.updateDoctor)
          } else {
            this.doctorService.addDoctor(this.doctor.Doctor)
          }
          this.router.navigateByUrl('cases');
        }
      }
    )


  }
}
