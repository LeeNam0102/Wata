using System;
using Wata.Commerce.Common.Objects;

namespace Wata.Commerce.Account.Domain.Filters
{
    public class UserLoginFilter : FilterBase
    {
		public string UserId { get; set; }

		public void Clear()
        {
			UserId = null;

        }
    }
}