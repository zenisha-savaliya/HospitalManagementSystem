using Data.Interface;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace Data.Repository
{
    public class AppoinmentRepository : IAppoinmentRepository
    {
        private readonly AppDbContext _context;
        public AppoinmentRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public async Task<bool> AddAppointment(Appointment appointment)
        {
            try
            {
                await _context.Appointments.AddAsync(appointment);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding a doctor: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ChangeStatus(int id, string status)
        {
            try
            {
                Appointment appointmentToUpdate = await _context.Appointments.FirstOrDefaultAsync(a => a.AppoinmentId == id);

                if (appointmentToUpdate == null)
                {
                    return false; 
                }

                appointmentToUpdate.Status = status;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Appointment>> CheckAppointments(string consultDoctor)
        {
            return await _context.Appointments
                .Where(a => a.ConsultDoctor == consultDoctor)
                .ToListAsync();
        }

        public async Task<bool> CheckDoctorAvailability(string consultDoctor, DateTime startTime)
        {
            var anyAppointments = await _context.Appointments
                .AnyAsync(a => a.ConsultDoctor == consultDoctor && a.ScheduleEndTime > startTime);
            return !anyAppointments;
        }

        public async Task<Appointment> GetAppointment(int id)
        {
            Appointment appointment = await _context.Appointments.FirstOrDefaultAsync(a => a.PatientId == id);
            return appointment;
        }

        public async Task<List<Appointment>> GetAppointmentList(int id)
        {
            return await _context.Appointments
                                    .Where(a => a.PatientId == id)
                                    .ToListAsync();
        }

        public async Task<bool> RemoveAppointment(Appointment appointment)
        {
            try
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while removing appointment: {ex.Message}");
                return false;
            }
        }
    }
}
