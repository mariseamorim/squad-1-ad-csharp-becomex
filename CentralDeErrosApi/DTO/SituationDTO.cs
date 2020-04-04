using System.ComponentModel.DataAnnotations;

namespace CentralDeErrosApi.DTO
{
    public class SituationDTO
    {
        public int SituationId { get; set; }

        [Required]
        public string SituationName { get; set; }
    }
}
