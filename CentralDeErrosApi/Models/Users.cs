using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralDeErrosApi.Models
{
    [Table("Users")]
    public class Users
    {
        [Column("Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Column("Name")]
        [StringLength(100)]
        [Required]
        public string Name { get; set; }

        [Column("Email")]
        [StringLength(100)]
        [Required]
        public string Email { get; set; }

        [Column("Password")]
        [StringLength(50)]
        [Required]
        public string Password { get; set; }

        [Column("Token")]
        [MaxLength(400)]
        [Required]
        public string Token { get; set; }

        [Column("Expiration")]
        [Required]
        public DateTime Expiration { get; set; }

        private ICollection<LogErrorOccurrence> ErrorOccurrences { get; set; }
    }
}
