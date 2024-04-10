namespace Service.DTO
{
    public class AssignDutyDTO
    {
        public int NurseId { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime PatientAdmittedTime { get; set; }
    }
}
