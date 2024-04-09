using Data.Interface;
using Data.Models;
using Service.DTO;
using Service.Interface;

namespace Service.Service
{
    public class NurseService : INurseService
    {
        private readonly IDutyRepository _dutyRepository;
        public NurseService(IDutyRepository dutyRepository)
        {
            _dutyRepository = dutyRepository;
        }
        public async Task<List<SeeDutyDTO>> SeeDuties(int id)
        {
            List<Duty>dutyList = await _dutyRepository.GetDutyList(id);

            List<SeeDutyDTO> seeDutyDTOList = dutyList.Select(duty => new SeeDutyDTO
            {
                DoctorId = duty.DoctorId,
                PatientId = duty.PatientId,
                PatientAdmittedTime = duty.AdmittedTime
            }).ToList();

            return seeDutyDTOList;
        }
    }
}
