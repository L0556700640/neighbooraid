import { Doctor } from './doctor.model';
import { CasesToDoctor } from './caseToDoctor.model';
import { Cases } from './cases.model';

export class DoctorDetails
{
  Doctor:Doctor;
  Cases  :Cases[];
  constructor()
  {
      this.Doctor=new Doctor();
      this.Cases=[];
  }
}

