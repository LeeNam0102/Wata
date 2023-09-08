using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wata.Commerce.Account.Domain.Models
{
	[Table("AspNetUserClaims")]
    public class UserClaim
    {
		[Key]
		[Required]
		public int Id{ get; set; }

		[StringLength(4000)]
		public string? ClaimType{ get; set; }

		[StringLength(4000)]
		public string? ClaimValue{ get; set; }

		[Required]
		[StringLength(450)]
		public string UserId{ get; set; }
    }
}