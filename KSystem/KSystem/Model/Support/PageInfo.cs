using KSystem.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KSystem.Model.Support
{
    public class PageInfo
    {
        public int PageNumber { get; set; }
        public int TotalItems { get; set; }
        public int PageSize { get; private set; }
        public int VisiblePages { get; private set; }
        public int TotalPages
        {
            get
            {
                return (int)Math.Ceiling((decimal)TotalItems / PageSize);
            }
        } // всего страниц

        public PageInfo()
        {
            PageNumber = 0;
            PageSize = AppSettings.GetSettings<int>("PageSize");
            VisiblePages = AppSettings.GetSettings<int>("VisiblePages");
        }
    }
}