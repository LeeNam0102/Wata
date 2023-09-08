using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wata.Commerce.Account.Domain.Models
{
	[Table("AspNetUserRoles")]
    public class UserRole
    {
		[Key]
		[Required]
		[StringLength(450)]
		public string UserId{ get; set; }

		[Required]
		[StringLength(450)]
		public string RoleId{ get; set; }
    }
}