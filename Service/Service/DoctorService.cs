using Data.Interface;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Service.DTO;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public DoctorService(IDoctorRepository doctorRepository,IUserRepository userRepository,INurseRepository nurseRepository,IReceptionistRepository receptionistRepository,IPatientRepository patientRepository,IDutyRepository dutyRepository)
        {
            _doctorRepository = doctorRepository;
            _userRepository = userRepository;
            _nurseRepository = nurseRepository;
            _receptionistRepository = receptionistRepository;
            _patientRepository = patientRepository;
            _dutyRepository = dutyRepository;
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
                    return "Doctor added successfully";
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
                    return "Nurse added successfully";
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
                    return "Receptionist added successfully";
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
    }
}

