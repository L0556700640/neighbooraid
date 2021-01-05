import { Component, OnInit } from '@angular/core';
import { CasesService } from 'src/app/shared/services/cases.service';

@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.component.html',
  styleUrls: ['./contacts.component.scss'],
})
export class ContactsComponent implements OnInit {
 case;
  constructor(private casesService:CasesService) { }

  ngOnInit() {
    this.case=this.casesService.CurrnetCase
  }

}
