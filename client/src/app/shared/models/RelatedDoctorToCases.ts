import { closeDoctor } from "./closeDoctor.model";
import { Contact } from "./contacts.model";

export class RelatedDoctorToCases 
{
    contacts  :Contact[];
    closeDoctor:closeDoctor[];
    constructor()
    {
        this.contacts=[];
        this.closeDoctor=[];
    }

}

