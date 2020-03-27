using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CentralDeErrosApi.DTO
{
    public class RegisterUserViewModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "O campo {0} é Obrigatório!")]
        public string Name { get; set; }


        [Required(ErrorMessage = "O campo {0} é Obrigatório!")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é Obrigatório!")]
        [StringLength(100, ErrorMessage = "O campo deve conter entre {2} e {1} caracteres", MinimumLength = 5)]
        public string Senha { get; set; }

        [Compare("Senha", ErrorMessage = "As senhas não conferem.")]
        public string ConfirmaSenha { get; set; }

        
    }
}
