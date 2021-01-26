import { Component, OnInit } from '@angular/core';
import { CasesService } from 'src/app/shared/services/cases.service';

@Component({
  selector: 'app-location',
  templateUrl: './location.component.html',
  styleUrls: ['./location.component.scss'],
})
export class LocationComponent implements OnInit {
  case;
  constructor(private casesService:CasesService) { }

  ngOnInit() {
    this.case=this.casesService.CurrnetCase
  }
}
