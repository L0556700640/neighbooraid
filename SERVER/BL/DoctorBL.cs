using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class DoctorBL
    {
        public static bool AddDoctor(DTO.Doctor doctor)
        {
            try
            {
                using(neighboorAidDBEntities db=new neighboorAidDBEntities())
                {
                    db.Doctors.Add(
                        Convertors.DoctorConvertor.ConvertDoctorToDAL(doctor)
                        );
                    db.SaveChanges();
                }
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}
