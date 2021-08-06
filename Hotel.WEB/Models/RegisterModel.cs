using Hotel.WEB.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hotel.WEB.Models
{
    public class RegisterModel
    {
        [Required]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        [RegularExpression(@".{8,}", ErrorMessage = "Password should contain at least 8 symbols")]
        [Required]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [Compare("Password",ErrorMessage ="Passwords didn`t match")]
        public string RepeatPassword { get; set; }

        public string HashedPassword
        {
            get
            {
                return Crypto.Hash(this.Password);
            }
        }
    }
}