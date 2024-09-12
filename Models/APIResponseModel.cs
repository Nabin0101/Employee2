using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.ViewModel.ResponseModel
{
    public class APIResponseModel
    {
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = "Successfully executed.";
        public object? Data { get; set; }
    }
}
