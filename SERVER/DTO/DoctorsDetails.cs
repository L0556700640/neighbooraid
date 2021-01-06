using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DoctorsDetailsDTO
    {
        public Doctor Doctor { get; set; }
        public Cases[] Cases { get; set; }
    }
}
