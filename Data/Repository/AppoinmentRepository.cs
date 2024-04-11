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
            try
            {
                return await _context.Appointments
                    .Where(a => a.ConsultDoctor == consultDoctor)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new List<Appointment>();
            }
        }

        public async Task<bool> CheckDoctorAvailability(string consultDoctor, DateTime startTime)
        {
            try
            {
                var anyAppointments = await _context.Appointments
                    .AnyAsync(a => a.ConsultDoctor == consultDoctor && a.ScheduleEndTime > startTime);
                return !anyAppointments;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<Appointment> GetAppointment(int id)
        {
            try
            {
                Appointment appointment = await _context.Appointments.FirstOrDefaultAsync(a => a.PatientId == id);
                return appointment;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task<List<Appointment>> GetAppointmentList(int id)
        {
            try
            {
                return await _context.Appointments
                    .Where(a => a.PatientId == id)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new List<Appointment>(); 
            }
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
