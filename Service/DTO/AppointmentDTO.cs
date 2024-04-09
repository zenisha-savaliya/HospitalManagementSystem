using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTO
{
    public class AppointmentDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string? PostalCode { get; set; }
        public string PatientProblem { get; set; }
        public string Description { get; set; }
        public string PatientId { get; set; }
        public DateTime ScheduleStartTime { get; set; }
        public string Status { get; set; }
        public string ConsultDoctor { get; set; }

    }
}
