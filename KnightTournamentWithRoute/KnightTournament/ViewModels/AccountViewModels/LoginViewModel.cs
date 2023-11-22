using System.ComponentModel.DataAnnotations;

namespace KnightTournament.ViewModels.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
