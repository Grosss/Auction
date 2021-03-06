﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MvcPL.Models
{
    public class RegisterViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Enter your e-mail")]
        [Required(ErrorMessage = "The field can not be empty!")]
        [StringLength(50)]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Invalid email")]
        public string Email { get; set; }

        [Display(Name = "Enter your login")]
        [Required(ErrorMessage = "The field can not be empty!")]
        [StringLength(50)]
        public string Login { get; set; }

        [Display(Name = "Enter your First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Enter your Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Enter your password")]
        [StringLength(128, ErrorMessage = "The password must contain at least {2} characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Enter your password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm the password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm the password")]
        [Compare("Password", ErrorMessage = "Passwords must match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Enter the captcha")]
        [Display(Name = "Enter the code from the image")]
        public string Captcha { get; set; }
    }
}