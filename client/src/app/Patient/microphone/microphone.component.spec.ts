import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { MicrophoneComponent } from './microphone.component';

describe('MicrophoneComponent', () => {
  let component: MicrophoneComponent;
  let fixture: ComponentFixture<MicrophoneComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MicrophoneComponent ],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(MicrophoneComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
