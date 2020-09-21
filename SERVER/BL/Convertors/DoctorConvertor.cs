using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Convertors
{
    public class DoctorConvertor
    {
        public static DTO.Doctor ConvertDoctorToDTO(DAL.Doctor myDoctor)
        {
            return new DTO.Doctor
            {
                doctorId = myDoctor.doctorId,
                address = myDoctor.address,
                doctorPhone=myDoctor.doctorPhone,
                firstName=myDoctor.firstName,
                isConfirmed=myDoctor.isConfirmed,
                lastName=myDoctor.lastName,
                pictureDiploma=myDoctor.pictureDiploma
            };
        }
        public static DAL.Doctor ConvertDoctorToDAL(DTO.Doctor myDoctor)
        {
            return new DAL.Doctor
            {
                doctorId = myDoctor.doctorId,
                address = myDoctor.address,
                doctorPhone = myDoctor.doctorPhone,
                firstName = myDoctor.firstName,
                isConfirmed = myDoctor.isConfirmed,
                lastName = myDoctor.lastName,
                pictureDiploma = myDoctor.pictureDiploma
            };
        }
    }
}
