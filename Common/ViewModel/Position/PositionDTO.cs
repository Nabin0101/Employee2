using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.ViewModel.Position
{
    public class PositionDTO
    {
        [Required]
        [RegularExpression(@"^[A-Za-z]+( [A-Za-z]+)$", ErrorMessage = "PositionName should contain alphabetic values only")]
        public string PositionName { get; set; }
    }
}
