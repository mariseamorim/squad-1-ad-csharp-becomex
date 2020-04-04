using System;
using System.ComponentModel.DataAnnotations;

namespace CentralDeErrosApi.DTO
{
    public class LogErrorOccurrenceDTO
    {
        [Required]
        public int ErrorId { get; set; }
        [Required]
        public string Origin { get; set; }

        [Required]
        public string Details { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        //[Required]
      //  public int UserId { get; set; }

        [Required]
        public int SituationId { get; set; }

        [Required]
        public int EnvironmentId { get; set; }

        [Required]
        public int CodeError { get; set; }

        [Required]
        public int LevelId { get; set; }

        [Required]
        public string Title { get; set; }

    }
}
