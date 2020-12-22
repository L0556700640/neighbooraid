import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { IonDatetime } from '@ionic/angular';
import { DoctorDetails } from 'src/app/shared/models/doctorDelails.model';
import { DoctorService } from 'src/app/shared/services/doctor.service';
import { LoginService } from 'src/app/shared/services/login.service';

@Component({
  selector: 'app-diploma',
  templateUrl: './diploma.component.html',
  styleUrls: ['./diploma.component.scss'],
})
export class DiplomaComponent implements OnInit {
  imagePath;
  imageProvider;
  myForm: FormGroup;
  doctor: DoctorDetails = new DoctorDetails();
  user:DoctorDetails = new DoctorDetails();
  constructor(private doctorService: DoctorService,private router: Router,private loginService:LoginService) { }

  ngOnInit() {
    this.myForm = new FormGroup({
      diploma: new FormControl(),
      certificateNumber:new FormControl(),
      certificateValidity:new FormControl(),

   });
   this.doctorService.getCurrentDoctor(this.loginService.CurrnetUser).subscribe(
    res => {
      if (this.loginService.IsLogin) {
        this.user.Doctor = res;
        this.myForm.controls.certificateNumber.setValue(this.user.Doctor.certificateNumber)
        this.myForm.controls.certificateValidity.setValue(this.user.Doctor.certificateValidity)
        this.myForm.controls.diplomaName.setValue(this.user.Doctor.diploma.name)
      }

    });
  }
  next() {
    this.doctorService.finishAddDoctor(this.imagePath,this.doctor).subscribe(
      (res) => { alert(res) }
    )
    this.router.navigateByUrl('finish')
  }

  changeListener($event): void 
  {
    this.imagePath = $event.target.files[0];
    console.log($event.target.files[0])
    //this.imageProvider.uploadImage(this.imagePath)
  }
  openDateTimePicker(picker: IonDatetime){
    picker.open().then(() => {
    }).catch(() => {
    })
  }
  
}
