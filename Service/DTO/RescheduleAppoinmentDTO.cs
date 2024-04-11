namespace Service.DTO
{
    public class RescheduleAppoinmentDTO
    {
        public int AppoinmentId { get; set; }
        public string Status { get; set; }
        public DateTime NewStartTime { get; set; }
        public DateTime NewEndTime { get; set; }

    }
}
