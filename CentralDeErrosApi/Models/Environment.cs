using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralDeErrosApi.Models
{
    [Table("Environment")]
    public class Environment
    {
        [Column("ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EnvironmentId { get; set; }

        [Column("Environment")]
        [StringLength(30)]
        [Required]
        public string EnvironmentName { get; set; }


    }
}
