using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AppoinmentId { get; set; }

        [Required]
        [StringLength(30)]
        public string PatientProblem { get; set; }

        [Required]
        [ForeignKey("Patient")]
        public int PatientId { get; set; }

        public virtual Patient Patient{ get; set; }

        [StringLength(40)]
        public string? Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ScheduleStartTime { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ScheduleEndTime { get; set; }
        [Required]
        [StringLength(20)]
        public string Status { get; set; }
        [Required]
        [StringLength(20)]
        public string ConsultDoctor { get; set; }

    }
}
