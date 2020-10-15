﻿using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BL
{
    public class DoctorBL
    {
        public static int AddDoctor(DTO.DoctorsDetailsDTO doctor,HttpPostedFile file)
        {
            try
            {
                using (neighboorAidDBEntities db=new neighboorAidDBEntities())
                {
                    DAL.Doctor newDoctor = Convertors.DoctorConvertor.ConvertDoctorToDAL(doctor.Doctor);
                    using (var binaryReader = new BinaryReader(file.InputStream))
                    {
                     newDoctor.pictureDiploma= binaryReader.ReadBytes(file.ContentLength);
                    }
                    db.Doctors.Add(newDoctor);
                    db.SaveChanges();
                    //לבדוק אם נכון להעביר בצורה כזו למסד הנתונים (בלי המרה) ב
                    foreach (var c in doctor.Cases)
                    {
                        db.CasesToDoctors.Add(
                        new DAL.CasesToDoctor
                        {
                            doctorId = newDoctor.doctorId,
                            caseId = c.caseId,
                            satisfaction = 0
                        }
                        ) ;
                    }
                    db.SaveChanges();
                    ManagerBL.SendMailToConfirmDoctor(newDoctor);

                    return newDoctor.doctorId;}
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return -1;
            }
        }

        public static void ConfirmDoctor(int doctorId, bool isConfirmed)
        {
            using (neighboorAidDBEntities db = new neighboorAidDBEntities())
            {
                (from d in db.Doctors
                 where d.doctorId == doctorId
                 select d).ToList().ForEach(d => d.isConfirmed = isConfirmed);
                db.SaveChanges();
            }

         }
    }
}
