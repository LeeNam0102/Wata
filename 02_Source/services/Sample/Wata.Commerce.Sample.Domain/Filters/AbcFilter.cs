using System;
using Wata.Commerce.Common.Objects;

namespace Wata.Commerce.Sample.Domain.Filters
{
    public class AbcFilter : FilterBase
    {
		public DateTime? FromDate { get; set; }
		public DateTime? ToDate { get; set; }

		public void Clear()
        {
			FromDate = null;
			ToDate = null;

        }
    }
}