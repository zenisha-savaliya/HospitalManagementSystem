using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Duty
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DutyId { get; set; }

        [ForeignKey("Nurse")]
        [Required]
        public int NurseId { get; set; }

        [JsonIgnore]
        public virtual Nurse Nurse { get; set; }
        [ForeignKey("Doctor")]
        [Required]
        public int DoctorId { get; set; }

        [JsonIgnore]
        public virtual Doctor Doctor { get; set; }

        [ForeignKey("Patient")]
        [Required]
        public int PatientId { get; set; }
        [JsonIgnore]
        public virtual Patient Patient { get; set; }
        public DateTime AdmittedTime {  get; set; }
    }
}
