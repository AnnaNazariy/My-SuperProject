using System;
using System.Collections.Generic;
using System.Linq;

namespace NtierEF.API
{
    public static class PagedListExtensions
        {
            public static PagedList<T> ToPagedList<T>(this IQueryable<T> source, int pageNumber, int pageSize)
            {
                var count = source.Count();
                var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                return new PagedList<T>(items, count, pageNumber, pageSize);
            }
        }

        public class PagedList<T>
        {
            public List<T> Items { get; private set; }
            public int TotalCount { get; private set; }
            public int PageNumber { get; private set; }
            public int PageSize { get; private set; }
            public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

            public PagedList(List<T> items, int count, int pageNumber, int pageSize)
            {
                Items = items;
                TotalCount = count;
                PageNumber = pageNumber;
                PageSize = pageSize;
            }
        }
    }

