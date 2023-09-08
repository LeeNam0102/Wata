using System;
using Wata.Commerce.Common.Objects;

namespace Wata.Commerce.Account.Domain.Filters
{
    public class RoleClaimFilter : FilterBase
    {
		public string RoleId { get; set; }

		public void Clear()
        {
			RoleId = null;

        }
    }
}