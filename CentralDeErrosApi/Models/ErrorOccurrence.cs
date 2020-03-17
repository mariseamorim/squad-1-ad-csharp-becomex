using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CentralDeErrosApi.Models
{
    [Table("ErrorOccurrence")]
    public class ErrorOccurrence
    {
        [Column("Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ErrorOccurrenceId { get; set; }

        [Column("Origin")]
        [StringLength(200)]
        [Required]
        public string Origin { get; set; }

        [Column("Details")]
        [StringLength(2000)]
        [Required]
        public string Details { get; set; }

        [Column("Date_Time")]
        [Required]
        public DateTime DateTime { get; set; }

        [ForeignKey("User_Id"), Required]
        public int UserId { get; set; }

        [Column("User_Id"), Required]
        public Users User { get; set; }// referencia 

        [ForeignKey("Error_Id"), Required]
        public int ErrorId { get; set; }

        [Column("Error_Id"), Required]
        public Error Error { get; set; }// referencia 

        [ForeignKey("Situation_Id"), Required]
        public int SituationId { get; set; }

        [Column("Situation_Id"), Required]
        public Situation Situation { get; set; }// referencia 
    }
}
