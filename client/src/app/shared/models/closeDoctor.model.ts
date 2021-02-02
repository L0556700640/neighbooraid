import { Doctor } from "./doctor.model";

export class closeDoctor 
{
    Doctor:Doctor;
    DistanceInMinutesFromPatient:number
    Satisfaction:number
    constructor()
    {
        this.Doctor=new Doctor();
    }
}

