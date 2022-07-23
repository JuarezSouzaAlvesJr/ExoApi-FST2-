using System.ComponentModel.DataAnnotations;

namespace ExoApi_FST2_.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Senha { get; set; }
    }
}
