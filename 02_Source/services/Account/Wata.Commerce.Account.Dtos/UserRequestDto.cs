using System;
using System.ComponentModel.DataAnnotations;
using Wata.Commerce.Common.Objects;

namespace Wata.Commerce.Account.Dtos
{
    public partial class UserRequestDto : RequestDtoBase
    {
		[Required]
		[StringLength(450)]
		public string Id{ get; set; }

		[Required]
		public int AccessFailedCount{ get; set; }

		[StringLength(4000)]
		public string? ConcurrencyStamp{ get; set; }

		[StringLength(256)]
		public string? Email{ get; set; }

		[Required]
		public bool EmailConfirmed{ get; set; }

		[Required]
		public bool LockoutEnabled{ get; set; }

		public bool LockoutEnd{ get; set; }

		[StringLength(256)]
		public string? NormalizedEmail{ get; set; }

		[StringLength(256)]
		public string? NormalizedUserName{ get; set; }

		[StringLength(4000)]
		public string? PasswordHash{ get; set; }

		[StringLength(4000)]
		public string? PhoneNumber{ get; set; }

		[Required]
		public bool PhoneNumberConfirmed{ get; set; }

		[StringLength(4000)]
		public string? SecurityStamp{ get; set; }

		[Required]
		public bool TwoFactorEnabled{ get; set; }

		[StringLength(256)]
		public string? UserName{ get; set; }
		//put your code here

    }
}