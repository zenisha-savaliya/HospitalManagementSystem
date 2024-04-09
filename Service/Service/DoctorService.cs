using Data.Interface;
using Data.Models;
using Data.Repository;
using Microsoft.EntityFrameworkCore;
using Service.DTO;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IUserRepository _userRepository;
        private readonly INurseRepository _nurseRepository;
        private readonly IReceptionistRepository _receptionistRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IDutyRepository _dutyRepository;
        private readonly IAppoinmentRepository _appoinmentRepository;
        private readonly IEmailService _emailService;

        public DoctorService(IDoctorRepository doctorRepository,IUserRepository userRepository,INurseRepository nurseRepository,IReceptionistRepository receptionistRepository,IPatientRepository patientRepository,IDutyRepository dutyRepository,IAppoinmentRepository appoinmentRepository,IEmailService emailService)
        {
            _doctorRepository = doctorRepository;
            _userRepository = userRepository;
            _nurseRepository = nurseRepository;
            _receptionistRepository = receptionistRepository;
            _patientRepository = patientRepository;
            _dutyRepository = dutyRepository;
            _appoinmentRepository = appoinmentRepository;
            _emailService = emailService;
        }
        public async Task<string> AddDoctor(RegisterDTO registerDTO, string Specialization)
        {
            try
            {
                if (await _doctorRepository.CheckSpecialization(Specialization))
                {
                    return "Doctor already exists with this specialization";
                }
                else if (await _doctorRepository.GetDoctorCount() >= 3)
                {
                    return "System already has three doctors";
                }
                else
                {
                    User user = new User
                    {
                        FirstName = registerDTO.FirstName,
                        LastName = registerDTO.LastName,
                        Password = registerDTO.Password,
                        ContactNumber = registerDTO.ContactNumber,
                        Email = registerDTO.Email,
                        DateOfBirth = registerDTO.DateOfBirth,
                        Gender = registerDTO.Gender,
                        PostalCode = registerDTO.PostalCode,
                        Role = "Doctor"
                    };

                    int userId = await _userRepository.RegisterUser(user);

                    Doctor doctor = new Doctor
                    {
                        FirstName = registerDTO.FirstName,
                        LastName = registerDTO.LastName,
                        Password = registerDTO.Password,
                        ContactNumber = registerDTO.ContactNumber,
                        Email = registerDTO.Email,
                        DateOfBirth = registerDTO.DateOfBirth,
                        Gender = registerDTO.Gender,
                        PostalCode = registerDTO.PostalCode,
                        Specialist = Specialization,
                        UserId = userId
                    };
                    await _doctorRepository.AddDoctor(doctor);
                    EmailDTO emailDTO = new EmailDTO
                    {
                        ToEmail = registerDTO.Email,
                        Subject = "registering into our system as doctor",
                        Body = $"<h4><b>Dear {registerDTO.FirstName},</b></h4><br><br>" +
                                $"Welcome to our service. Your current password is <span style=\"color:blue;\">{registerDTO.Password}</span>. " +
                                $"You can login using this password and can change your password."
                    };
                    bool isAdded = await _emailService.SendEmailAsync(emailDTO.ToEmail, emailDTO.Subject, emailDTO.Body);
                    if(isAdded)
                    {
                        return "Doctor added successfully";
                    }
                    return "doctor not added successfully";
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding a doctor: {ex.Message}");
                return "Error while adding a doctor";
            }
        }

        public async Task<string> AddNurse(RegisterDTO registerDTO)
        {
            try
            {
                if (await _nurseRepository.GetNurseCount() >= 10)
                {
                    return "System already has 10 nurses";
                }
                else
                {
                    User user = new User
                    {
                        FirstName = registerDTO.FirstName,
                        LastName = registerDTO.LastName,
                        Password = registerDTO.Password,
                        ContactNumber = registerDTO.ContactNumber,
                        Email = registerDTO.Email,
                        DateOfBirth = registerDTO.DateOfBirth,
                        Gender = registerDTO.Gender,
                        PostalCode = registerDTO.PostalCode,
                        Role = "Nurse"
                    };
                    int userId = await _userRepository.RegisterUser(user);
                    Nurse nurse = new Nurse
                    {
                        FirstName = registerDTO.FirstName,
                        LastName = registerDTO.LastName,
                        Password = registerDTO.Password,
                        ContactNumber = registerDTO.ContactNumber,
                        Email = registerDTO.Email,
                        DateOfBirth = registerDTO.DateOfBirth,
                        Gender = registerDTO.Gender,
                        PostalCode = registerDTO.PostalCode,
                        UserId = userId
                    };

                    await _nurseRepository.AddNurse(nurse);
                    EmailDTO emailDTO = new EmailDTO
                    {
                        ToEmail = registerDTO.Email,
                        Subject = "registering into our system as nurse",
                        Body = $"<h4><b>Dear {registerDTO.FirstName},</b></h4><br><br>" +
                                $"Welcome to our service. Your current password is <span style=\"color:blue;\">{registerDTO.Password}</span>. " +
                                $"You can login using this password and can change your password."
                    };
                    bool isAdded = await _emailService.SendEmailAsync(emailDTO.ToEmail, emailDTO.Subject, emailDTO.Body);
                    if (isAdded)
                    {
                        return "Nurse added successfully";
                    }
                    return "Nurse not added";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding a doctor: {ex.Message}");
                return "Error while adding a nurse";
            }
        }
        public async Task<string> AddReceptionist(RegisterDTO registerDTO)
        {
            try
            {
                if (await _receptionistRepository.GetReceptionistCount() >= 2)
                {
                    return "System already has 2 receptionists";
                }
                else
                {
                    User user = new User
                    {
                        FirstName = registerDTO.FirstName,
                        LastName = registerDTO.LastName,
                        Password = registerDTO.Password,
                        ContactNumber = registerDTO.ContactNumber,
                        Email = registerDTO.Email,
                        DateOfBirth = registerDTO.DateOfBirth,
                        Gender = registerDTO.Gender,
                        PostalCode = registerDTO.PostalCode,
                        Role = "Receptionist"
                    };
                    int userId = await _userRepository.RegisterUser(user);
                    Receptionist nurse = new Receptionist
                    {
                        FirstName = registerDTO.FirstName,
                        LastName = registerDTO.LastName,
                        Password = registerDTO.Password,
                        ContactNumber = registerDTO.ContactNumber,
                        Email = registerDTO.Email,
                        DateOfBirth = registerDTO.DateOfBirth,
                        Gender = registerDTO.Gender,
                        PostalCode = registerDTO.PostalCode,
                        UserId = userId
                    };

                    await _receptionistRepository.AddReceptionist(nurse);
                    EmailDTO emailDTO = new EmailDTO
                    {
                        ToEmail = registerDTO.Email,
                        Subject = "registering into our system as receptionist",
                        Body = $"<h4><b>Dear {registerDTO.FirstName},</b></h4><br><br>" +
                         $"Welcome to our service. Your current password is <span style=\"color:blue;\">{registerDTO.Password}</span>. " +
                         $"You can login using this password and can change your password."
                    };
                    bool isAdded = await _emailService.SendEmailAsync(emailDTO.ToEmail, emailDTO.Subject, emailDTO.Body);
                    if (isAdded)
                    {
                       return "Receptionist added successfully";
                    }
                    return "Receptionist not added";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding a receptionist: {ex.Message}");
                return "Error while adding a Receptionist";
            }
        }

        public async Task<string> AssignDuty(AssignDutyDTO assignDutyDTO)
        {
            bool NurseExist = await _nurseRepository.CheckNurseExist(assignDutyDTO.NurseId);
            if(!NurseExist)
            {
                return "nurse not exist";
            }
            bool PatientExist = await _patientRepository.CheckPatientExistById(assignDutyDTO.PatientId);
            if (!PatientExist)
            {
                return "patient not exist";
            }
            bool DoctorExist = await _doctorRepository.CheckDoctorExist(assignDutyDTO.DoctorId);
            if (!DoctorExist)
            {
                return "doctor not exist";
            }
            if(NurseExist && PatientExist && DoctorExist)
            {
                Duty duty = new Duty
                {
                    NurseId = assignDutyDTO.NurseId,
                    DoctorId = assignDutyDTO.DoctorId,
                    PatientId = assignDutyDTO.PatientId,
                    AdmittedTime = assignDutyDTO.PatientAdmittedTime
                };
                bool result = await _dutyRepository.AddDuty(duty);
                if(result)
                {
                    return "duty assigned to nurse";
                }
                else
                {
                    return "error while assigning nurse";
                }
            }
            return "error while assigning nurse";
        }

        public async Task<string> ChangeStatus(int id, string status)
        {
            bool result = await _appoinmentRepository.ChangeStatus(id, status);
            if(result)
            {
                return "status changed successfully";
            }
            else
            {
                return "error while updating status";
            }
        }

        public async Task<List<DoctorAppointmentViewDTO>> CheckAppointments(string consultDoctor)
        {
            List<Appointment> appointmentList = await _appoinmentRepository.CheckAppointments(consultDoctor);
            List<DoctorAppointmentViewDTO> doctorAppointments = appointmentList
            .Select(a => new DoctorAppointmentViewDTO
            {
                PatientProblem = a.PatientProblem,
                PatientId = a.PatientId.ToString(), 
                ScheduleStartTime = a.ScheduleStartTime,
                Status = a.Status
            })
            .ToList();

            return doctorAppointments;

        }
    }
}

