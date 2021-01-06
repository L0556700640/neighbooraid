import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AlertController } from '@ionic/angular';
import { Cases } from 'src/app/shared/models/cases.model';
import { CasesToDoctor } from 'src/app/shared/models/caseToDoctor.model';
import { DoctorDetails } from 'src/app/shared/models/doctorDelails.model';
import { CasesService } from 'src/app/shared/services/cases.service';
import { DoctorService } from 'src/app/shared/services/doctor.service';
import { LoginService } from 'src/app/shared/services/login.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent implements OnInit {
  user: DoctorDetails = new DoctorDetails()
  casesToDoctor: CasesToDoctor[];
  cases: Cases[];
  nameCases:string[]=[];
  constructor(private loginService: LoginService, private router: Router, private doctorService: DoctorService, private casesService: CasesService, private alertController: AlertController) { }

  ngOnInit() {

    this.doctorService.getCurrentDoctor(this.loginService.CurrnetUser).subscribe(
      res => this.user.Doctor = res
    )

    this.doctorService.getCurrentCasesToDoctor(this.loginService.CurrnetUser).subscribe(

      res => {this.casesToDoctor = res; }

    )
    this.casesService.getAllCases().subscribe(
      res=>
      {
        
        this.cases=res;
        this.casesToDoctor.forEach(ctd => 
        {
          this.cases.forEach(c => 
          {
            if(ctd.caseId==c.caseId)
            {
              this.nameCases.push(c.caseName)
              let s=ctd.satisfaction.toString()+"%"
              document.getElementById(c.caseId.toString()).style.width=s
            }
          });
        });
      }
    )


    // console.log(this.user)
  }

  updateDoctor() {

    this.router.navigate(['/personalInformation']);
  }

  async deleteDoctor() {

    // this.doctorService.delete(this.loginService.CurrnetUser).subscribe(
    //   res => alert(res)
    // )
    const alert = await this.alertController.create({
      //cssClass: 'my-custom-class',
      header: 'Confirm!',
      message: 'Message <strong>text</strong>!!!',
      buttons: [
        {
          text: 'Cancel',
          role: 'cancel',
          // cssClass: 'secondary',
          handler: () => {
            console.log('Confirm Cancel: blah');
          }
        }, {
          text: 'Okay',
          handler: () => {
            console.log('Confirm Okay');
          }
        }
      ]
    });

    await alert.present();

  }
}
