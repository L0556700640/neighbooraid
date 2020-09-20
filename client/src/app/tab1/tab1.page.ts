import { Component } from '@angular/core';

@Component({
  selector: 'app-tab1',
  templateUrl: 'tab1.page.html',
  styleUrls: ['tab1.page.scss']
})
export class Tab1Page {
  page1=true;
  page2=false;
  page3=false; 
  finish=false;
  public form = [
      { val: 'כויות', isChecked: false },
      { val: 'חתכים', isChecked: false },
      { val: 'מכות יבשות', isChecked: false },
      { val: 'התחשמלות', isChecked: false },
      { val: 'עקיצה', isChecked: false },
      { val: 'פריחה', isChecked: false },
      { val: 'הקשה', isChecked: false },
      { val: 'נשיכה', isChecked: false },
      { val: 'התקף חרדה', isChecked: false },
      { val: 'רעלים', isChecked: false },
      { val: 'חום', isChecked: false }
    ];
  constructor() {}

  next1()
  {
    this.page1=false;
    this.page2=true;
  }
  next2()
  {
    this.page2=false;
    this.page3=true;
  }

  next3()
  {
    this.page3=false;
    this.finish=true;
  }

  previous1()
  {
    this.page1=true;
    this.page2=false;
  }

  previous2()
  {
    this.page2=true;
    this.page3=false;
  }
}

