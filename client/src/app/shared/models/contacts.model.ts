import { Doctor } from "./doctor.model";

export class Contact
{
    Doctor:Doctor;
    Name:string
    Satisfaction:number
    constructor()
    {
        this.Doctor=new Doctor();
    }
}

