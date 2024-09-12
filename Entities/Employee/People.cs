using Data_Access_Layer.Model;
using System.ComponentModel.DataAnnotations;

namespace Entities.Employee
{
    public class People : BaseEntity
    {


        public string FirstName { get; set; }

        public string MiddleName { get; set; }
        public string LastName { get; set; }


        public string Address { get; set; }
        public string Email { get; set; }

        public Employee Employee { get; set; }

    }
}

