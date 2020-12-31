import { Doctor } from './doctor.model';
import { CasesToDoctor } from './caseToDoctor.model';

export class DoctorDetails
{
  Doctor:Doctor;
  Cases  :CasesToDoctor[];
  constructor()
  {
      this.Doctor=new Doctor();
      this.Cases=[];
  }
}

