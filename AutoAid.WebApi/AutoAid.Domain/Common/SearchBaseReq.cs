using AutoAid.Domain.Common.PagedList;

namespace AutoAid.Domain.Common
{
    public class SearchBaseReq
    {
        public string? KeySearch { get; set; } = null;
        public PagingQuery PagingQuery { get; set; } = new PagingQuery();
        public string? OrderBy { get; set; } = null;
    }
}
