using Data.Interface;
using Data.Models;
using Service.DTO;
using Service.Interface;
using System.Text.RegularExpressions;

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
            string validationMessage = ValidateAppointmentDTO(appointmentDTO);
            if (!string.IsNullOrEmpty(validationMessage))
            {
                return validationMessage;
            }
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
        private bool IsValidEmail(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, emailPattern);
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
        private string ValidateAppointmentDTO(AppointmentDTO appointmentDTO)
        {
            if (string.IsNullOrEmpty(appointmentDTO.FirstName) ||
            string.IsNullOrEmpty(appointmentDTO.LastName) ||
            string.IsNullOrEmpty(appointmentDTO.ContactNumber) ||
            string.IsNullOrEmpty(appointmentDTO.Email) ||
            appointmentDTO.DateOfBirth == default ||
            string.IsNullOrEmpty(appointmentDTO.Gender)  ||
            string.IsNullOrEmpty(appointmentDTO.PatientProblem) ||
            string.IsNullOrEmpty(appointmentDTO.PatientId) ||
            appointmentDTO.ScheduleStartTime == default ||
            string.IsNullOrEmpty(appointmentDTO.Status) ||
            string.IsNullOrEmpty(appointmentDTO.ConsultDoctor))
            {
                return "All fields are required.";
            }
            if (appointmentDTO.DateOfBirth >= DateTime.Now)
            {
                return "Date of birth should be before the current date.";
            }
            if (!IsValidEmail(appointmentDTO.Email))
            {
                return "Invalid email format.";
            }
            if (!Regex.IsMatch(appointmentDTO.ContactNumber, @"^\d{10}$"))
            {
                return "Contact number should be of 10 digits.";
            }
            if (!Regex.IsMatch(appointmentDTO.PatientId, "^Sterling_[0-9]+$"))
            {
                return "Invalid patient ID format.";
            }
            if (appointmentDTO.Gender.ToLower() != "male" && appointmentDTO.Gender.ToLower() != "female" && appointmentDTO.Gender.ToLower() != "other")
            {
                return "Invalid gender. Gender should be Male, Female, or Other.";
            }
            List<string> validStatusList = new List<string> {"Scheduled", "Cancelled", "Rescheduled" };
            if (!validStatusList.Contains(appointmentDTO.Status))
            {
                return "InValid Status Type";
            }
            return null;
        }
    }
}
