import { closeDoctor } from "./closeDoctor.model";
import { Contact } from "./contacts.model";

export class RelatedDoctorToCases 
{
    contactsDoctor  :Contact[];
    closeDoctor:closeDoctor[];
    constructor()
    {
        this.contactsDoctor=[];
        this.closeDoctor=[];
    }

}

