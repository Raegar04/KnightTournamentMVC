﻿using KnightTournament.Models.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace KnightTournament.ViewModels.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? Email { get; set; }
        //public List<string> ActionsEmailConfirming { get; set; } = new List<string>();
        //public string SelectedActionEmailConfirming { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = $"Value is too short", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string? ConfirmPassword { get; set; }

        public Rank Rank { get; set; }

        public double Rating { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
