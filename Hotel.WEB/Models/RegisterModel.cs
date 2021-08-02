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
        [Required]
        [RegularExpression(@".{8,}",ErrorMessage ="Password should contain at least 8 symbols")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
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