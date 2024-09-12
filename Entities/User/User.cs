using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access_Layer.Model;

namespace Entities.User
{
    public class User : BaseEntity
    {



        public string FirstName { get; set; }

        public string LastName { get; set; }


        public string UserName { get; set; }


        public string Email { get; set; }

        public string Password { get; set; }


        public string ConfirmPassword { get; set; }

        public string Address { get; set; }
        public Token Token { get; set; }


    }
}
