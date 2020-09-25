using System;
using System.Collections.Generic;
using WebApiLayer.Models;

namespace MvcPresentationLayer.ViewModel
{
    public class IndexViewModel
    {
        public List<EmployeeDTO> Employees { get; set; }
        public PageModel PageModel { get; set; }

        public IndexViewModel()
        {

        }
    }
}
