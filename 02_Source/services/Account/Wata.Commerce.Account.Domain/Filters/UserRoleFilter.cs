using System;
using Wata.Commerce.Common.Objects;

namespace Wata.Commerce.Account.Domain.Filters
{
    public class UserRoleFilter : FilterBase
    {
		public string UserId { get; set; }
		public string RoleId { get; set; }

		public void Clear()
        {
			UserId = null;
			RoleId = null;

        }
    }
}