using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wata.Commerce.Account.Domain.Models
{
	[Table("AspNetRoles")]
    public class Role
    {
		[Key]
		[Required]
		[StringLength(450)]
		public string Id{ get; set; }

		[StringLength(4000)]
		public string? ConcurrencyStamp{ get; set; }

		[StringLength(256)]
		public string? Name{ get; set; }

		[StringLength(256)]
		public string? NormalizedName{ get; set; }
    }
}