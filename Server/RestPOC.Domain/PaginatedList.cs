namespace RestPOC.Domain {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPageCount { get; private set; }

        public PaginatedList(
            int pageIndex, int pageSize,
            int totalCount, IQueryable<T> source)
        {
            this.AddRange(source);

            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.TotalCount = totalCount;
            this.TotalPageCount =
                (int)Math.Ceiling(totalCount / (double)pageSize);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (this.PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (this.PageIndex < this.TotalPageCount);
            }
        }
    }
}