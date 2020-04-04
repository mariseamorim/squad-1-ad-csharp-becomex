﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CentralDeErrosApi.Models
{
        [Table("Level")]
        public class Level
        {
            [Column("Id")]
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int LevelId { get; set; }

            [Column("Level")]
            [StringLength(105)]
            [Required]
            public string LevelName { get; set; }
        }
    }

