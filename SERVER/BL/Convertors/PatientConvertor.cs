using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Convertors
{
    public static class PatientConvertor
    {
        public static DTO.Patient ConvertPatientToDTO(DAL.Patient patient)
        {
            return new DTO.Patient
            {
                patientId = patient.patientId,
                patientPhone = patient.patientPhone
            };
        }
        public static DAL.Patient ConvertPatientToDAL(DTO.Patient patient)
        {
            return new DAL.Patient
            {
                patientId = patient.patientId,
                patientPhone = patient.patientPhone
            };
        }
    }
}
