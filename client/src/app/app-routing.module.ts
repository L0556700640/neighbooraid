import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { from } from 'rxjs';
import { GpsComponent } from './gps/gps.component';
import { MicrophoneComponent } from './Patient/microphone/microphone.component';
import{LoginComponent} from './Doctor/login/login.component'
import{PersonalInformationComponent} from './Registration/personal-information/personal-information.component'
import{CasesComponent} from './Registration/cases/cases.component'
import { FinishComponent } from './Registration/finish/finish.component';
import { MoreCasesComponent } from './Patient/more-cases/more-cases.component';
import { ContactsComponent } from './Patient/contacts/contacts.component';
import { LocationComponent } from './Patient/location/location.component';
import { CriticismComponent } from './Patient/criticism/criticism.component';
import { ProfileComponent } from './Doctor/profile/profile.component';
import { DiplomaComponent } from './Registration/diploma/diploma.component';
import { CallComponent } from './Patient/call/call.component';
import { FinishPatiantComponent } from './Patient/finish-patiant/finish-patiant.component';

const routes: Routes = [
    // path: '',
    // loadChildren: () => import('./tabs/tabs.module').then(m => m.TabsPageModule)
    {path:'',component:GpsComponent},

    {path:'microphone',component:MicrophoneComponent},
    {path:'moreCases',component:MoreCasesComponent},
    {path:'contacts',component:ContactsComponent},
    {path:'location',component:LocationComponent},
    {path:'criticism',component:CriticismComponent},
    {path:'call',component:CallComponent},
    {path:'finishPatiant',component:FinishPatiantComponent},

    {path:'profile',component:ProfileComponent},
    {path:'login',component:LoginComponent}, 

    {path:'personalInformation',component:PersonalInformationComponent},
    {path:'cases',component:CasesComponent},
    {path:'diploma',component:DiplomaComponent},
    {path:'finish',component:FinishComponent},
];



@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {}
