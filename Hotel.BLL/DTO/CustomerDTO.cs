using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.DTO
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Passport { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is CustomerDTO)
            {
                var objCM = obj as CustomerDTO;
                return //this.Id == objCM.Id
                     this.FirstName == objCM.FirstName
                    && this.MiddleName == objCM.MiddleName
                    && this.Passport == objCM.Passport
                    && this.DateOfBirth == objCM.DateOfBirth;
            }
            else return base.Equals(obj);

        }
    }
}
