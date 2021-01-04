import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { Cases } from 'src/app/shared/models/cases.model';
import { CasesService } from 'src/app/shared/services/cases.service';
import { DoctorService } from 'src/app/shared/services/doctor.service';

@Component({
  selector: 'app-cases',
  templateUrl: './cases.component.html',
  styleUrls: ['./cases.component.scss'],
})
export class CasesComponent implements OnInit {
  allCases: Cases[] = [];
  myForm: FormGroup;
  isChoose: boolean[] = [];
  len = 0;
  i = 0;
  splitedCases: [Cases[]] = [[]]
  casesToDoctor: Cases[] = []

  constructor(private casesService: CasesService, private router: Router, private doctorService: DoctorService) {
    this.casesService.getAllCases().subscribe(res => {
      this.allCases = res;
      //     this.len=Math.round(res.length/3) ;
      //     if(res.length%3!=0)
      //     this.len++;
      //   console.log(res)
      // console.log(res.length) ;
      let i = 0;
      for (; i < res.length - 2; i += 3) {
        this.splitedCases.push([res[i], res[i + 1], res[i + 2]]);
        this.isChoose.push(false)
      }
      if (i < this.allCases.length) {
        this.splitedCases.push([]);
        for (; i < res.length; i++) {
          this.splitedCases[this.splitedCases.length - 1].push(this.allCases[i]);
        }
        console.log(this.splitedCases)


      }
      for (let i = 0; i < this.allCases.length; i++) {
        this.isChoose.push(false);
      }
    });


  }

  ngOnInit() {



  }

  isChoosen(i, j) {
    i--;
    console.log(this.isChoose[i * 3 + j])
    return this.isChoose[i * 3 + j]
  }
  clickCases(i, j) {
    i--;
    console.log(i + ' ' + j)
    console.log(i * 3 + j)
    this.isChoose[i * 3 + j] = !this.isChoose[i * 3 + j]
   

  }
  // add() {

  //   if(this.i<this.allCases.length)
  //   return this.i++;
  //   return this.i;

  // }

  addCases() {
    for (let i = 0; i < this.allCases.length; i++) {
       if (this.isChoose[i] == true) 
    {
      this.casesToDoctor.push(this.allCases[i])
    }
    }
   
    
    this.doctorService.AddCasesToDoctor(this.casesToDoctor)
    this.router.navigateByUrl('diploma')
  }
}
