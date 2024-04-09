using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface
{
    public interface IAppoinmentRepository
    {
        Task<bool> AddAppointment(Appointment appointment);
        Task<bool> CheckDoctorAvailability(string consultdoctor, DateTime starttime);
        Task<Appointment> GetAppointment(int id);
        Task<List<Appointment>> GetAppointmentList(int id);
        Task<List<Appointment>> CheckAppointments(string consultDoctor);
        Task<bool> ChangeStatus(int id, string status);
    }
}
