using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTO
{
    public class AssignDutyDTO
    {
        public int NurseId { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime PatientAdmittedTime { get; set; }
    }
}
