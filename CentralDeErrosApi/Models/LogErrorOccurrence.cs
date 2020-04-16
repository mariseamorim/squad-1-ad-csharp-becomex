using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CentralDeErrosApi.Models
{
    [Table("LogErrorOccurrence")]
    public class LogErrorOccurrence
    {
        [Column("Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ErrorId { get; set; }

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

       /* [Column("USER_ID"), Required]
        public int  UserId { get; set; }

        [Column("USER_ID"), Required]
        public Users User { get; set; }// referencia */

        [Column("Situation_Id"), Required]
        public int SituationId { get; set; }

        [ForeignKey("Environment_Id"), Required]
        public int Environmente_Id { get; set; }

        [Column("CodeErro")]
        [Required]
        public int Code { get; set; }

        [ForeignKey("Level_Id"), Required]
        public int LevelId { get; set; }

        [Column("Title")]
        [StringLength(200)]
        [Required]
        public string Title { get; set; }

        [Column("SITUATION_ID"), Required]
        public Situation Situation { get; set; }// referencia 

    }
}
