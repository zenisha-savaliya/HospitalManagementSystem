using Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IDoctorService
    {

        Task<string> AddDoctor(RegisterDTO registerDTO,string Specialization);
        Task<string> AddNurse(RegisterDTO registerDTO);
        Task<string> AddReceptionist(RegisterDTO registerDTO);
        Task<string> AssignDuty(AssignDutyDTO assignDutyDTO);

    }
}
