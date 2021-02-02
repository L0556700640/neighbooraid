import { closeDoctor } from "./closeDoctor.model";
import { Contact } from "./contacts.model";

export class Cases 
{
    contacts  :Contact[];
    closeDoctors:closeDoctor[];
    constructor()
    {
        this.contacts=[];
        this.closeDoctors=[];
    }

}

