using Data.Models;
using Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IReceptionistService
    {
        Task<string> ScheduleAppoinment(AppointmentDTO appointmentDTO);
    }
}
