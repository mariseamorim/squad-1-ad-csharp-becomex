using System.ComponentModel.DataAnnotations;

namespace CentralDeErrosApi.Models.ViewModels
{
    public class LoginUserViewModel
    {
        [Required(ErrorMessage = "O campo {0} é Obrigatório!")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é Obrigatório!")]
        [StringLength(100, ErrorMessage = "O campo deve conter entre {2} e {1} caracteres", MinimumLength = 5)]
        public string Password { get; set; }
    }
}
