using System;
using Wata.Commerce.Common.Objects;

namespace Wata.Commerce.Sample.Dtos
{
    public class AbcFilterDto : FilterBase
    {
		public DateTime? FromDate { get; set; }
		public DateTime? ToDate { get; set; }

    }
}