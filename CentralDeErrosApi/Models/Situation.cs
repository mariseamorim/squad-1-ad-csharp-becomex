using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CentralDeErrosApi.Models
{
    [Table("Situation")]
    public class Situation
    {
        [Column("Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SituationId { get; set; }

        [Column("Situation")]
        [StringLength(30)]
        [Required]
        public string SituationName { get; set; }

        public ICollection<ErrorOccurrence> ErrorOccurrences { get; set; }
    }
}
