﻿using System;
namespace WebApiLayer.Models
{
    public class PagingParameterModel
    {
        const int maxPageSize = 20;

        public int pageNumber { get; set; } = 1;

        public int _pageSize { get; set; } = 5;

        public int pageSize
        {

            get { return _pageSize; }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
