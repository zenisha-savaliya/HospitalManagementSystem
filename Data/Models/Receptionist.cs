using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Data.Models
{
    public class Receptionist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReceptionistId { get; set; }
        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20)]
        public string LastName { get; set; }

        [Required]
        [StringLength(64)]
        public string Password { get; set; }

        [Required]
        [Phone]
        public string ContactNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(6)]
        public string Gender { get; set; }

        [MinLength(5)]
        [MaxLength(10)]
        public string? PostalCode { get; set; }

        [ForeignKey("User")]
        [Required]
        public int UserId { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }

    }
}
