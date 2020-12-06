import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AlertController } from '@ionic/angular';
import { DoctorDetails } from 'src/app/shared/models/doctorDelails.model';
import { DoctorService } from 'src/app/shared/services/doctor.service';
import { LoginService } from 'src/app/shared/services/login.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent implements OnInit {
  user: DoctorDetails = new DoctorDetails()
  constructor(private loginService: LoginService,private router:Router, private doctorService: DoctorService, private alertController: AlertController) { }

  ngOnInit() {

    this.doctorService.getCurrentDoctor(this.loginService.CurrnetUser).subscribe(
      res => this.user.Doctor = res
    )
    // console.log(this.user)
  }

  updateDoctor()
  {
    
    this.router.navigate(['/personalInformation']);
  }

  deleteDoctor() {

    this.doctorService.delete(this.loginService.CurrnetUser).subscribe(
      res => alert(res)
    )
    // const alert = await this.alertController.create({
    //   cssClass: 'my-custom-class',
    //   header: 'Confirm!',
    //   message: 'Message <strong>text</strong>!!!',
    //   buttons: [
    //     {
    //       text: 'Cancel',
    //       role: 'cancel',
    //       cssClass: 'secondary',
    //       handler: (blah) => {
    //         console.log('Confirm Cancel: blah');
    //       }
    //     }, {
    //       text: 'Okay',
    //       handler: () => {
    //         console.log('Confirm Okay');
    //       }
    //     }
    //   ]
    // });

    // await alert.present();

  }
}
