using System;
using Wata.Commerce.Common.Objects;

namespace Wata.Commerce.Account.Domain.Filters
{
    public class UserClaimFilter : FilterBase
    {
		public string UserId { get; set; }

		public void Clear()
        {
			UserId = null;

        }
    }
}