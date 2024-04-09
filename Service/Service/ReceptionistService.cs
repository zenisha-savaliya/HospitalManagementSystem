using Data.Interface;
using Data.Models;
using Data.Repository;
using Service.DTO;
using Service.Interface;

namespace Service.Service
{
    public class ReceptionistService : IReceptionistService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IUserRepository _userRepository;
        private readonly INurseRepository _nurseRepository;
        private readonly IReceptionistRepository _receptionistRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IAppoinmentRepository _appoinmentRepository;

        public ReceptionistService(IDoctorRepository doctorRepository,IUserRepository userRepository,INurseRepository nurseRepository,IReceptionistRepository receptionistRepository,IPatientRepository patientRepository,IAppoinmentRepository appoinmentRepository)
        {
            _doctorRepository = doctorRepository;
            _userRepository = userRepository;
            _nurseRepository = nurseRepository;
            _receptionistRepository = receptionistRepository;
            _patientRepository = patientRepository;
            _appoinmentRepository = appoinmentRepository;
        }
        public async Task<string> ScheduleAppoinment(AppointmentDTO appointmentDTO)
        {
            bool doctorExists = await _doctorRepository.CheckSpecialization(appointmentDTO.ConsultDoctor);
            if (!doctorExists)
            {
                return $"Doctor with specialization '{appointmentDTO.ConsultDoctor}' does not exist in our system.";
            }
            bool doctorAvailable = await _appoinmentRepository.CheckDoctorAvailability(appointmentDTO.ConsultDoctor, appointmentDTO.ScheduleStartTime);

            if (!doctorAvailable)
            {
                return "Doctor is not available for this slot.";
            }
            int patientId;
            bool patientExists = await _patientRepository.CheckPatientExist(appointmentDTO.Email, appointmentDTO.FirstName);
            if (patientExists)
            {
                patientId = await _patientRepository.GetPatientId(appointmentDTO.Email,appointmentDTO.FirstName);
                Appointment appointment = new Appointment
                {
                    PatientId = patientId,
                    PatientProblem = appointmentDTO.PatientProblem,
                    Description = appointmentDTO.Description,
                    ScheduleStartTime = appointmentDTO.ScheduleStartTime,
                    ScheduleEndTime = appointmentDTO.ScheduleStartTime.AddHours(1),
                    Status = appointmentDTO.Status,
                    ConsultDoctor = appointmentDTO.ConsultDoctor,
                };
                await _appoinmentRepository.AddAppointment(appointment);
                return "Appointment scheduled successfully.";
            }
            else
            {
                string password = GeneratePassword(appointmentDTO.FirstName, appointmentDTO.DateOfBirth);
                User user = new User
                {
                    FirstName = appointmentDTO.FirstName,
                    LastName = appointmentDTO.LastName,
                    Password = password,
                    ContactNumber = appointmentDTO.ContactNumber,
                    Email = appointmentDTO.Email,
                    DateOfBirth = appointmentDTO.DateOfBirth,
                    Gender = appointmentDTO.Gender,
                    PostalCode = appointmentDTO.PostalCode,
                    Role = "Patient"
                };
                int userId = await _userRepository.RegisterUser(user);
                Patient patient = new Patient
                {
                    FirstName = appointmentDTO.FirstName,
                    LastName = appointmentDTO.LastName,
                    Password = password,
                    ContactNumber = appointmentDTO.ContactNumber,
                    Email = appointmentDTO.Email,
                    DateOfBirth = appointmentDTO.DateOfBirth,
                    Gender = appointmentDTO.Gender,
                    PostalCode = appointmentDTO.PostalCode,
                    UserId = userId
                };
                await _patientRepository.RegisterPatient(patient);
                int patientIdFromDTO;

                string numericPart = ExtractNumericPart(appointmentDTO.PatientId);
                int.TryParse(numericPart, out patientIdFromDTO);

                Appointment appointment = new Appointment
                {
                    PatientId = patientIdFromDTO,
                    PatientProblem = appointmentDTO.PatientProblem,
                    Description = appointmentDTO.Description,
                    ScheduleStartTime = appointmentDTO.ScheduleStartTime,
                    ScheduleEndTime = appointmentDTO.ScheduleStartTime.AddHours(1),
                    Status = appointmentDTO.Status,
                    ConsultDoctor = appointmentDTO.ConsultDoctor,
                };
                await _appoinmentRepository.AddAppointment(appointment);
                return "Patient profile created and appointment scheduled successfully.";
            }
        }

        private string GeneratePassword(string firstName, DateTime dateOfBirth)
        { 
            string birthDatePart = dateOfBirth.ToString("MMddyyyy"); 
            return $"{firstName}{birthDatePart}";
        }
        private string ExtractNumericPart(string patientId)
        {
            string[] parts = patientId.Split('_');
            if (parts.Length == 2)
            {
                return parts[1]; 
            }
            return patientId;
        }
    }
}
