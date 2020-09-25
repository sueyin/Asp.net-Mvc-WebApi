using System;
namespace WebApiLayer.Models
{
    public class PageModel
    {
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        public PageModel()
        {

        }
        public PageModel(int totalPages, int currentPage, int pageSize)
        {
            TotalPages = totalPages;
            CurrentPage = currentPage;
            PageSize = pageSize;
        }
    }
}
