import { IonicModule } from '@ionic/angular';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Tab1Page } from './tab1.page';
import { ExploreContainerComponentModule } from '../explore-container/explore-container.module';

import { Tab1PageRoutingModule } from './tab1-routing.module';
import {AgmCoreModule} from '@agm/core'
import {GooglePlaceModule} from 'ngx-google-places-autocomplete'
import {NgxLoadingModule, ngxLoadingAnimationTypes} from 'ngx-loading'


@NgModule({
  imports: [
    IonicModule,
    CommonModule,
    FormsModule,
    ExploreContainerComponentModule,
    Tab1PageRoutingModule
  ],
  declarations: [Tab1Page],
  providers: [],
  bootstrap: [Tab1Page]
})
export class Tab1PageModule {}
