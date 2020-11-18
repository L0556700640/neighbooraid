import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { DoctorDetails } from 'src/app/shared/models/doctorDelails.model';
import { DoctorService } from 'src/app/shared/services/doctor.service';

@Component({
  selector: 'app-diploma',
  templateUrl: './diploma.component.html',
  styleUrls: ['./diploma.component.scss'],
})
export class DiplomaComponent implements OnInit {
  imagePath;
  imageProvider;
  myForm: FormGroup;
  // doctor: DoctorDetails = new DoctorDetails();
  constructor(private doctorService: DoctorService,private router: Router) { }

  ngOnInit() {}
  next() {
    this.doctorService.finishAddDoctor(this.imagePath).subscribe(
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
  
}
