using KnightTournament.Models.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace KnightTournament.ViewModels.AccountViewModels
{
    public class RegisterViewModel
    {
        public string UserName { get; set; }
        public string? Email { get; set; }

        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }

        public Rank User_Rank { get; set; }

        public double User_Rating { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
