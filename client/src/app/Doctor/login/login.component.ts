import { Route } from '@angular/compiler/src/core';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Doctor } from 'src/app/shared/models/doctor.model';
import { DoctorService } from 'src/app/shared/services/doctor.service';
import { LoginService } from 'src/app/shared/services/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  loginForm; 
  failedMessage = false;   
  constructor(private doctorService: DoctorService,private loginService:LoginService,private router:Router) { }

  ngOnInit()
  {
    this.loginForm = new FormGroup({       
      username: new FormControl('', Validators.required),       
      password: new FormControl('', Validators.required)     
    });   
  }

  checkUser() 
  {     
    const firstName = this.loginForm.controls.username.value;     
    const doctorId = this.loginForm.controls.password.value;  
    this.loginService.setCurrentUser(doctorId)   
    this.loginService.chackUser(firstName,doctorId).subscribe(
      res=>
      {
        if(res)
        this.router.navigate(['/profile']);
        else
        {
          this.failedMessage = true;       
          this.loginForm.reset();     
        }  
         
      }
    )
  } 
  Registration(){
    this.loginService.setCurrentUser(undefined)
    this.router.navigate(['/personalInformation']);

  }
}
