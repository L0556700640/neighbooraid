using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Convertors
{
    public static class HelpCallConvertor
    {
        public static DTO.HelpCall ConvertHelpCallToDTO(DAL.HelpCall helpCall)
        {
            return new DTO.HelpCall
            {
                addressX = helpCall.addressX,
                addressY = helpCall.addressY,
                callId = helpCall.callId,
                date = helpCall.date,
                doctorId = helpCall.doctorId
            };
        }
        public static DAL.HelpCall ConvertHelpCallToDAL(DTO.HelpCall helpCall)
        {
            return new DAL.HelpCall
            {
                addressX = helpCall.addressX,
                addressY = helpCall.addressY,
                callId = helpCall.callId,
                date = helpCall.date,
                doctorId = helpCall.doctorId
            };
        }
    }
}
