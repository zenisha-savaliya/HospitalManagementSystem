using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTO
{
    public  class SeeDutyDTO
    {
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime PatientAdmittedTime { get; set; }
    }
}
