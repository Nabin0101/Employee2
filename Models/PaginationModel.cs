
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.ViewModel.ResponseModel
{
    public class PaginationModel
    {
        private int _pageSize = 4;
        private int _pageNumber = 1;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > 0 ? value : _pageSize;
        }
        public int PageNumber
        {
            get => _pageNumber;
            set => _pageNumber = value > 0 ? value : _pageNumber;
        }
        public string? Filter { get; set; }
    }
}
