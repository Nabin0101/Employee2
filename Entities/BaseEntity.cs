using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Model
{
    public  class BaseEntity
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

    }
}
