import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Address } from 'ngx-google-places-autocomplete/objects/address';
import { Cases } from 'src/app/shared/models/cases.model';
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
  constructor(private doctorService: DoctorService, private casesService: CasesService) {
    this.casesService.getAllCases().subscribe(res => { this.allCases = res; });
  }

  ngOnInit() { }

  next() {
    this.doctorService.addDoctor(this.doctor, this.imagePath).subscribe(
      (res) => { alert(res) }
    )
  }

  changeListener($event): void 
  {
    this.imagePath = $event.target.files[0];
    console.log($event.target.files[0])
    //this.imageProvider.uploadImage(this.imagePath)
  }
  
  handleDestinationChange(a: Address) {
    this.doctor.Doctor.address = (a.formatted_address);
    console.log(a)
  }
}
