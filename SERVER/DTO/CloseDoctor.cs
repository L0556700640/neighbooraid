using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CloseDoctor
    {
        public DTO.Doctor Doctor { get; set; } = new Doctor();
        public int DistanceInMinutesFromPatient { get; set; }
        public int Satisfaction  { get; set; }
        public CloseDoctor()
        {

        }

        public CloseDoctor(Doctor doctor, int distanceInMinutesFromPatient, int satisfaction)
        {
            Doctor = doctor;
            DistanceInMinutesFromPatient = distanceInMinutesFromPatient;
            Satisfaction = satisfaction;
        }
    }
}
