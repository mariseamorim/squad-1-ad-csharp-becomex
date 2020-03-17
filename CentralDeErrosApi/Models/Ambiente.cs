using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralDeErrosApi.Models
{
    [Table("Ambiente")]
    public class Ambiente
    {
        [Column("Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EnvironmentId { get; set; }

        [Column("Ambiente")]
        [StringLength(30)]
        [Required]
        public string AmbienteName { get; set; }

        public ICollection<Error> Errors { get; set; }

    }
}
