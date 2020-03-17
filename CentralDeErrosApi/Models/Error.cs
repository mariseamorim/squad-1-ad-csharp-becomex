using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralDeErrosApi.Models
{
    [Table("Error")]
    public class Error
    {
        [Column("Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ErrorId { get; set; }

        [Column("Codee")]
        [Required]
        public int Code { get; set; }

        [Column("Title")]
        [StringLength(200)]
        [Required]
        public string Title { get; set; }

        [Column("Description")]
        [StringLength(200)]
        [Required]
        public string Description { get; set; }

        [ForeignKey("Ambiente_Id"), Required]
        public int EAmbiente_Id { get; set; }

        [Column("Ambiente_Id"), Required]
        public Ambiente Ambiente { get; set; }// referencia 

        [ForeignKey("Level_Id"), Required]
        public int LevelId { get; set; }

        [Column("Level_Id"), Required]
        public Level Level { get; set; }// referencia 

    }
}
