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

@NgModule({
  declarations: [AppComponent],
  entryComponents: [],
  imports: [
    BrowserModule,
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
    { provide: RouteReuseStrategy, useClass: IonicRouteStrategy }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
