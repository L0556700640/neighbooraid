import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { Cases } from 'src/app/shared/models/cases.model';
import { CasesService } from 'src/app/shared/services/cases.service';

@Component({
  selector: 'app-cases',
  templateUrl: './cases.component.html',
  styleUrls: ['./cases.component.scss'],
})
export class CasesComponent implements OnInit {
  allCases: Cases[] = [];
  myForm: FormGroup;
  isChoose: boolean = true;
  len=0;
  i = 0;

  constructor(private casesService: CasesService, private router: Router) {
    this.casesService.getAllCases().subscribe(res => { this.allCases = res;
    this.len=Math.round(res.length/3) ;
    if(res.length%3!=0)
    this.len++;
  console.log(res)
console.log(res.length) ;
});
    
    
  }

  ngOnInit() {

    
    
  }

  clickCases() {
    this.isChoose = !this.isChoose
    this.i=0
    console.log(this.isChoose)  
        
  }
  add() {
    return this.i++;
  }
}
