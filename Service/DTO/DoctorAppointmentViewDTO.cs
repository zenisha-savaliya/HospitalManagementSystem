namespace Service.DTO
{
    public class DoctorAppointmentViewDTO
    {
        public string PatientProblem { get; set; }
        public string PatientId { get; set; }
        public DateTime ScheduleStartTime { get; set; }
        public string Status { get; set; }

    }
}
