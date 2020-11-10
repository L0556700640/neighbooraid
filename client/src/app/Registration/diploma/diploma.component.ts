import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-diploma',
  templateUrl: './diploma.component.html',
  styleUrls: ['./diploma.component.scss'],
})
export class DiplomaComponent implements OnInit {
  imagePath;
  imageProvider;
  myForm: FormGroup;
  constructor() { }

  ngOnInit() {}
  changeListener($event): void 
  {
    this.imagePath = $event.target.files[0];
    console.log($event.target.files[0])
    //this.imageProvider.uploadImage(this.imagePath)
  }
}
