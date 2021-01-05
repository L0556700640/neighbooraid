import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup } from '@angular/forms';
import { Cases } from 'src/app/shared/models/cases.model';
import { CasesService } from 'src/app/shared/services/cases.service';

@Component({
  selector: 'app-more-cases',
  templateUrl: './more-cases.component.html',
  styleUrls: ['./more-cases.component.scss'],
})
export class MoreCasesComponent implements OnInit {

  allCases: Cases[] = [];
  myForm: FormGroup;
  isChoose: boolean[] = [];
  len = 0;
  i = 0;
  splitedCases: [Cases[]] = [[]]
  casesToDoctor: Cases[] = []

  constructor(private casesService: CasesService, private router: Router) {
    this.casesService.getAllCases().subscribe(res => {
      this.allCases = res;
     
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
      // for (let i = 0; i < this.allCases.length; i++) {
      //   this.isChoose.push(false);
      // }
    });

  
    
  }

  ngOnInit() {

    
    
  }
  clickCases(i,j) 
  {
    i--;
    this.casesService.setCurrentCase(this.allCases[i * 3 + j])
    this.router.navigateByUrl('contacts')
  }
    
  clickCorrectCase() {
    //to the server
        
  }

}


