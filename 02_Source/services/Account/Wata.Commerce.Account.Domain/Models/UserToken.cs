using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wata.Commerce.Account.Domain.Models
{
	[Table("AspNetUserTokens")]
    public class UserToken
    {
		[Key]
		[Required]
		[StringLength(450)]
		public string UserId{ get; set; }

		[Required]
		[StringLength(450)]
		public string LoginProvider{ get; set; }

		[Required]
		[StringLength(450)]
		public string Name{ get; set; }

		[StringLength(4000)]
		public string? Value{ get; set; }
    }
}