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

  clickCorrectCase() {
    //to the server
        
  }
  add() {
    return this.i++;
  }

}


