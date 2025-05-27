using EShop.Shared.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Shared.Domain.Paginations;
public class PagingRequest
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public SortOrder SortType { get; set; }
    public string ColName { get; set; } = "Id";
}