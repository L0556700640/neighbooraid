using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Convertors
{
    public static class CaseToDoctorConvertor
    {
        public static DTO.CasesToDoctor ConvertCasesToDoctorToDTO(DAL.CasesToDoctor casesToDoctor)
        {
            return new DTO.CasesToDoctor
            {
                caseId = casesToDoctor.caseId,
                doctorId = casesToDoctor.doctorId,
                satisfaction = casesToDoctor.satisfaction
            };
        }

        public static DAL.CasesToDoctor ConvertCasesToDoctorToDAL(DTO.CasesToDoctor casesToDoctor)
        {
            return new DAL.CasesToDoctor
            {
                caseId = casesToDoctor.caseId,
                doctorId = casesToDoctor.doctorId,
                satisfaction = casesToDoctor.satisfaction
            };
        }

    }
}
