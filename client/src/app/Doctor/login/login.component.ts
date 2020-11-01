import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  loginForm; 
  constructor() { }

  ngOnInit()
  {
    this.loginForm = new FormGroup({       
      username: new FormControl('', Validators.required),       
      password: new FormControl('', Validators.required)     
    });   
  }

}
