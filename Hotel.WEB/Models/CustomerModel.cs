using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hotel.WEB.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string MiddleName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Passport { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is CustomerModel)
            {
                var objCM = obj as CustomerModel;
                return
                     this.FirstName == objCM.FirstName
                    && this.MiddleName == objCM.MiddleName
                    && this.Passport == objCM.Passport
                    && this.DateOfBirth == objCM.DateOfBirth;
            }
            else return base.Equals(obj);

        }
    }
}