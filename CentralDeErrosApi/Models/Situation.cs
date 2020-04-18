using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
  
    }
}
