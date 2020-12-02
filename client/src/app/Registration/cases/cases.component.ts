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
  three='333';
  i=0
  constructor( private casesService: CasesService,private router: Router) 
  { 
    this.casesService.getAllCases().subscribe(res => { this.allCases = res; });
  }

  ngOnInit() {}

  clickCases()
  {
    
  }
  add()
  {
    
    return this.i++;
  }
}
