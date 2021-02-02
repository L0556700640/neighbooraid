import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouteReuseStrategy } from '@angular/router';
import { SpeechRecognition } from '@ionic-native/speech-recognition/ngx';
import { IonicModule, IonicRouteStrategy } from '@ionic/angular';
import { SplashScreen } from '@ionic-native/splash-screen/ngx';
import { StatusBar } from '@ionic-native/status-bar/ngx';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {HttpClientModule} from '@angular/common/http'
import { GooglePlaceModule } from 'ngx-google-places-autocomplete';
import { ngxLoadingAnimationTypes, NgxLoadingModule } from 'ngx-loading';
import { AgmCoreModule } from '@agm/core';
import{GpsComponent} from './gps/gps.component'
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MicrophoneComponent } from './Patient/microphone/microphone.component';
import { LoginComponent } from './Doctor/login/login.component';
import { PersonalInformationComponent } from './Registration/personal-information/personal-information.component';
import { CasesService } from './shared/services/cases.service';
import { CasesComponent } from './Registration/cases/cases.component';
import { MoreCasesComponent } from './Patient/more-cases/more-cases.component';
import { ContactsComponent } from './Patient/contacts/contacts.component';
import { LocationComponent } from './Patient/location/location.component';
import { CriticismComponent } from './Patient/criticism/criticism.component';
import { ProfileComponent } from './Doctor/profile/profile.component';
import { DiplomaComponent } from './Registration/diploma/diploma.component';
import { FinishComponent } from './Registration/finish/finish.component';
//import {Contacts} from '@ionic-native/contacts/ngx';
@NgModule({
  declarations: 
  [
    AppComponent,
    GpsComponent,
    MicrophoneComponent,
    MoreCasesComponent,
    ContactsComponent,
    LocationComponent,
    CriticismComponent,
    LoginComponent,
    ProfileComponent,
    PersonalInformationComponent,
    CasesComponent,
    DiplomaComponent,
    FinishComponent,
  ],
  entryComponents: [],
  imports: [
    ReactiveFormsModule,
    BrowserModule,
    FormsModule,
    IonicModule.forRoot(),
    GooglePlaceModule,
    AppRoutingModule,
    HttpClientModule,
    NgxLoadingModule.forRoot({
      animationType: ngxLoadingAnimationTypes.threeBounce,
      backdropBackgroundColour: 'rgba(0,0,0,0)', 
      backdropBorderRadius: '0.01px',
      primaryColour: 'green', 
      secondaryColour: 'green', 
      tertiaryColour: 'green'
    }),
    AgmCoreModule.forRoot(  {
      apiKey: 'AIzaSyD7Wui1ikC-x4CMLYBpPz-8Yutf2l3glNo',
     libraries: ["places"]
    })

  ],
  providers: [
    StatusBar,
    SplashScreen,
    SpeechRecognition,
    { provide: RouteReuseStrategy, useClass: IonicRouteStrategy },
    //Contacts
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
