using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Services
{
    public class PaginationTools
    {
        public static Tuple<int, int> ConvertPagesToCount(int page, int itemsPerPage)
        {
            if (page < 1)
                page = 1;

            if (itemsPerPage < 1)
                itemsPerPage = 10;

            return new Tuple<int, int>((page - 1) * itemsPerPage, itemsPerPage);
        }
    }
}
