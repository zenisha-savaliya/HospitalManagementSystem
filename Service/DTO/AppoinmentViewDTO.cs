using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTO
{
    public class AppoinmentViewDTO
    {
        public DateTime ScheduleStartTime { get; set; }
        public string Status { get; set; }
        public string ConsultDoctor { get; set; }
    }
}
