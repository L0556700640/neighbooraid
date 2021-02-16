import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { FinishPatiantComponent } from './finish-patiant.component';

describe('FinishPatiantComponent', () => {
  let component: FinishPatiantComponent;
  let fixture: ComponentFixture<FinishPatiantComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FinishPatiantComponent ],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(FinishPatiantComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
