using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CentralDeErrosApi.DTO
{
    public class LoginUserViewModel
    {
        [Required(ErrorMessage = "O campo {0} é Obrigatório!")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é Obrigatório!")]
        [StringLength(100, ErrorMessage = "O campo deve conter entre {2} e {1} caracteres", MinimumLength = 5)]
        public string Senha { get; set; }
    }
}
