import { Address } from 'ngx-google-places-autocomplete/objects/address'

export class Doctor {
    doctorId:string
    firstName:string
    lastName: string
    doctorPhone:string
    address: string;
    isConfirmed: boolean
    mail:string
    certificateNumber:string
    certificateValidity:Date
}