using EShop.Shared.Domain.Enums;
using EShop.Shared.Domain.Paginations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Shared.Application.Common.Paginations;
public static class PaginationUtils
{
    public static (int page, int pageSize, SortOrder sortType, string sortField) GetPaginationAndSortingValues(PagingRequest? request)
    {
        var page = request?.Page ?? 1;
        var pageSize = request?.PageSize ?? 10;
        var sortType = request?.SortType ?? SortOrder.Descending;
        var sortField = request?.ColName ?? "CreatedDate";

        return (page, pageSize, sortType, sortField);
    }
}
