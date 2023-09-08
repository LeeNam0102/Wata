using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wata.Commerce.Account.Domain.Models
{
	[Table("AspNetUserLogins")]
    public class UserLogin
    {
		[Key]
		[Required]
		[StringLength(450)]
		public string LoginProvider{ get; set; }

		[Required]
		[StringLength(450)]
		public string ProviderKey{ get; set; }

		[StringLength(4000)]
		public string? ProviderDisplayName{ get; set; }

		[Required]
		[StringLength(450)]
		public string UserId{ get; set; }
    }
}