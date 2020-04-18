using System.ComponentModel.DataAnnotations;

namespace CentralDeErrosApi.DTO
{
    public class LevelDTO
    {
        public int LevelId { get; set; }

        [Required]
        public string LevelName { get; set; }
    }
}
