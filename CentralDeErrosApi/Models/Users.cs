using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

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


    }
}
