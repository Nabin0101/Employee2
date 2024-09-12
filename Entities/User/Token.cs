using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access_Layer.Model;

namespace Entities.User
{
    public class Token : BaseEntity
    {

        public string? TokenName { get; set; }
        public string? UserId { get; set; }
        public User User { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsRevoked { get; set; }

    }
}
